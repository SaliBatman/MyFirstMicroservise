using System;
using System.Collections.Generic;
using System.Text;

namespace MyFirstMicroservice.Domain.Core.Events
{
    public abstract class Event
    {
        public DateTime TimeStamp { get; set; }
        public Event()
        {
            TimeStamp = DateTime.Now;
        }
    }
}
