using System;
using System.Collections.Generic;
using Example.Domain.Entities;

namespace Example.Repository.Repositories
{
    public class EventsRepository : IEventsRepository
    {
        private readonly List<Event> eventList = new List<Event> {
            new Event(new Guid("0ec5e125-5eee-4102-b48d-011a600fd74a"), "Road Trip 2000"),
            new Event(new Guid("8f8cfba1-b30e-4289-a7ca-3aa122adb30a"), "Mexico 1999")
        };

        public bool DoesTheEventExist(Guid id)
        {
            return this.eventList.Exists(b => b.Id.Equals(id));
        }

        public Event GetEventById(Guid id)
        {
            return this.eventList.Find(b => b.Id.Equals(id));
        }

        public List<Event> GetEventList()
        {
            return this.eventList;
        }

        public Event InsertEvent(Event Event)
        {
            Event.Id = Guid.NewGuid();
            this.eventList.Add(Event);
            return Event;
        }

        public List<Event> GetEventListForUser(Guid userId)
        {
            return this.eventList.FindAll(b => b.Creator.Id == userId);
        }

        public List<Subscription> GetSubscriptionsByEvent(Guid eventId)
        {
            return new List<Subscription>()
            {
                new Subscription()
                {
                    CreationDate = new DateTime(2000,12,31),
                    Event = new Event(Guid.NewGuid(), "Dummy Event"),
                    User = new User(Guid.NewGuid(), "Norris", "Chuck", new DateTime(1984, 07, 01))
                }
            };
        }
    }
}
