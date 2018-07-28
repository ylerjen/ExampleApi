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

        public Guid CreatorId { get; set; }

        public EventCategory Category { get; set; }
    }
}
