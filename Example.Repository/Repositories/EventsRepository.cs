using System;
using System.Collections.Generic;
using System.Text;
using Example.Domain.Entities;

namespace Example.Repository.Repositories
{
    public class EventsRepository : IEventsRepository
    {

        private readonly List<Event> EventList = new List<Event>() {
            new Event(new Guid("0ec5e125-5eee-4102-b48d-011a600fd74a"), "The Lord of the Rings : The two towers"),
            new Event(new Guid("8f8cfba1-b30e-4289-a7ca-3aa122adb30a"), "Game Of Thrones : Event 1")
        };

        public bool DoesTheEventExist(Guid id)
        {
            return this.EventList.Exists(b => b.Id.Equals(id));
        }

        public Event GetEventById(Guid id)
        {
            return this.EventList.Find(b => b.Id.Equals(id));
        }

        public List<Event> GetEventList()
        {
            return this.EventList;
        }

        public List<Event> GetEventListByAuthor(Guid userId)
        {
            return this.EventList.FindAll(b => b.CreatorId == userId);
        }

        public Event InsertEvent(Event Event)
        {
            Event.Id = Guid.NewGuid();
            this.EventList.Add(Event);
            return Event;
        }
    }
}
