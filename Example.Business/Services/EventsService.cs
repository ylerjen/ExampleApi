using System;
using System.Collections.Generic;
using Example.Domain.Entities;
using Example.Repository.Repositories;

namespace Example.Business.Services
{
    public class EventsService : IEventsService
    {
        private readonly IEventsRepository eventsRepository;

        public EventsService(IEventsRepository injectedEventsRepository)
        {
            this.eventsRepository = injectedEventsRepository;
        }

        public void EventExists(Guid guid)
        {
            throw new NotImplementedException();
        }

        public Event GetEventById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Event> GetEventsList()
        {
            throw new NotImplementedException();
        }

        public Event CreateEvent(Event eventObj)
        {
            throw new NotImplementedException();
        }

        public void DeleteEvent(Guid id)
        {
            throw new NotImplementedException();
        }

        public void UpdateEvent(Event eventObj)
        {
            throw new NotImplementedException();
        }

        public void ModifyEvent(string action, string prop, object newValue)
        {
            throw new NotImplementedException();
        }

        public List<Subscription> GetSubscriptionForEvent(Guid eventId)
        {
            return this.eventsRepository.GetSubscriptionsByEvent(eventId);
        }
    }
}
