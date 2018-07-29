using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Domain.Entities
{
    public class Event
    {
        public Event()
        {

        }

        public Event(Guid id, string title)
        {
            this.Id = id;
            this.Title = title;
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public EventCategory Category { get; set; }

        public List<Subscription> SubscriptionList { get; set; }

        public DateTime EventStart { get; set; }

        public DateTime EventEnd { get; set; }

        public User Creator { get; set; }
    }
}
