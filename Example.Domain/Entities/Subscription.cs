using System;

namespace Example.Domain.Entities
{
    public class Subscription
    {
        public User User { get; set; }

        public Event Event { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
