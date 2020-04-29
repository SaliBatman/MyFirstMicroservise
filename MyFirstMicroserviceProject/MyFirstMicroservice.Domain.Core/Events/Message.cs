using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyFirstMicroservice.Domain.Core.Events
{
    public abstract class Message  : IRequest<bool>
    {
        public string MessageType { get; set; }
        public Message()
        {
            MessageType = GetType().Name;
        }
    }
}
