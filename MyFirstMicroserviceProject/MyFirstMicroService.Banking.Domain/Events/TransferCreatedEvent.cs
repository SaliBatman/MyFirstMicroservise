using MyFirstMicroservice.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyFirstMicroService.Banking.Domain.Events
{
    public class TransferCreatedEvent : Event
    {
        public int To { get; set; }
        public int From { get; set; }
        public decimal Amount { get; set; }
        public TransferCreatedEvent(int from , int to , decimal amount)
        {
            To = to;
            From = from;
            Amount = amount;
        }
    }
}
