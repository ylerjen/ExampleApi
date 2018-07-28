using Example.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Repository.Repositories
{
    public interface IEventsRepository
    {
        bool DoesTheEventExist(Guid id);

        List<Event> GetEventList();

        Event GetEventById(Guid id);

        Event InsertEvent(Event author);

        List<Event> GetEventListByAuthor(Guid authorId);
    }
}
