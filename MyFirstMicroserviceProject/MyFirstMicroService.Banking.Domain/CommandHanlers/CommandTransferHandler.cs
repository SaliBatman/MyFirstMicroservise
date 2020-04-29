using MediatR;
using MyFirstMicroservice.Domain.Core.Bus;
using MyFirstMicroService.Banking.Domain.Commands;
using MyFirstMicroService.Banking.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFirstMicroService.Banking.Domain.CommandHanlers
{
    public class CommandTransferHandler : IRequestHandler<CreateTransferCommand, bool>
    {
        private readonly IEventBus _bus;
        public CommandTransferHandler(IEventBus bus)
        {

            _bus = bus;
        }
        public Task<bool> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
        {
            _bus.Publish(new TransferCreatedEvent(request.From,request.To,request.Amount));
            return Task.FromResult(true);
        }
    }
}
