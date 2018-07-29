using Example.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Example.Repository.Repositories
{
    public interface IEventsRepository
    {
        bool DoesTheEventExist(Guid id);

        List<Event> GetEventList();

        Event GetEventById(Guid id);

        Event InsertEvent(Event user);

        List<Event> GetEventListForUser(Guid userId);

        List<Subscription> GetSubscriptionsByEvent(Guid eventId);
    }
}
