using MyFirstMicroservice.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyFirstMicroservice.Domain.Core.Commands
{
    public abstract class  Command : Message
    {
        public DateTime TimeStamp { get; protected set ; }

        protected Command()
        {
            TimeStamp = DateTime.Now;
        }
    }
}
