using MyFirstMicroservice.Domain.Core.Commands;
using MyFirstMicroservice.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstMicroservice.Domain.Core.Bus
{
   public interface IEventBus
    {
        Task SendCommmand<T>(T Command) where T : Command;
        void Publish<T>(T @event) where T : Event;

        void Subscribe<T, TH>() where T : Event where TH : IEventHandler;
    }
}
