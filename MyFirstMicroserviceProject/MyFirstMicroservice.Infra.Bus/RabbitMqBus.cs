using MediatR;
using MyFirstMicroservice.Domain.Core.Bus;
using MyFirstMicroservice.Domain.Core.Commands;
using MyFirstMicroservice.Domain.Core.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyFirstMicroservice.Infra.Bus
{
    public sealed class RabbitMqBus : IEventBus
    {
        private readonly IMediator _mediator;
        private readonly Dictionary<string, List<Type>> handlers;
        private readonly List<Type> eventTypes;
        public RabbitMqBus(IMediator mediator)
        {
            _mediator = mediator;
            handlers = new Dictionary<string, List<Type>>();
            eventTypes = new List<Type>();
        }
        public void Publish<T>(T @event) where T : Event
        {
            var factory = new RabbitMQ.Client.ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var eventName = @event.GetType().Name;
                    channel.QueueDeclare(queue: eventName,
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

                    string message = JsonConvert.SerializeObject(@event);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey:eventName,
                                         basicProperties: null,
                                         body: body);
                }
            }
        }

        public async Task SendCommmand<T>(T Command) where T : Command
        {
            await _mediator.Send(Command);
        }

        public void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler
        {
            var eventName = typeof(T).Name;
            var handlerType = typeof(TH);
            if (!eventTypes.Contains(typeof(T)))
            {
                eventTypes.Add(typeof(T));
            }
            if (!handlers.ContainsKey(eventName))
            {
                handlers.Add(eventName, new List<Type>());
            }
            if (handlers[eventName].Any(s=> s.GetType() == handlerType))
            {
                throw new ArgumentException("alerdy registered ");
            }
        }
        private void StartBasicConsume<T>() where T : Event 
        {

            var factory = new ConnectionFactory()
            {
                UserName = "salar",
                Password = "salar",
                DispatchConsumersAsync = true
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var eventName = typeof(T).Name;

            channel.QueueDeclare(eventName, false, false, false, null);
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += Consumer_Recived;
            channel.BasicConsume(eventName, true, consumer);
        }

        private async Task Consumer_Recived(object sender, BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;
            var message = UTF8Encoding.UTF8.GetString(e.Body.Span.ToArray());
            try
            {
                await ProcessEvent(eventName, message).ConfigureAwait(false);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            if (handlers.ContainsKey(eventName))
            {
                var subscriptions = handlers[eventName];
                foreach (var item in subscriptions)
                {
                    var handler = Activator.CreateInstance(item);
                    if (handler == null) continue;
                    var eventType = eventTypes.SingleOrDefault(s => s.Name == eventName);
                    var @event = JsonConvert.DeserializeObject(message, eventType);
                    var conrtType = typeof(IEventHandler<>).MakeGenericType(eventType);
                   await (Task) conrtType.GetMethod("Handle").Invoke(handler, new object[] { @event });
                    
                }

            }
            throw new NotImplementedException();
        }
    }

}
