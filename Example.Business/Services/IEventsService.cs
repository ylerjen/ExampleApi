using System;
using System.Collections.Generic;
using Example.Domain.Entities;

namespace Example.Business.Services
{
    public interface IEventsService
    {
        void EventExists(Guid guid);

        Event GetEventById(Guid id);

        List<Event> GetEventsList();

        Event CreateEvent(Event eventObj);

        void DeleteEvent(Guid id);

        void UpdateEvent(Event eventObj);

        void ModifyEvent(string action, string prop, object newValue);

        List<Subscription> GetSubscriptionForEvent(Guid id);
    }
}
