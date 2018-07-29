using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Example.Business.Services;
using Example.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Example.Api.Controllers
{
    [Route("api/events")]
    public class EventsController : Controller
    {
        private IEventsService EventsServices { get; }

        public EventsController(IEventsService injectedEventsService)
        {
            this.EventsServices = injectedEventsService;
        }

        [HttpGet]
        public IActionResult GetEventsList()
        {
            var userList = this.EventsServices.GetEventsList();
            return this.Ok(userList);
        }

        [HttpGet]
        [Route("{id}/subscriptions")]
        public IActionResult GetSubscriptionForEvent(Guid eventId)
        {
            var subscriptionList = this.EventsServices.GetSubscriptionForEvent(eventId);
            return this.Ok(subscriptionList);
        }

        [HttpGet]
        [Route("{id}/users")]
        public IActionResult GetSubscribedUsersForEvent(Guid eventId)
        {
            var subscriptionList = this.EventsServices.GetSubscriptionForEvent(eventId);
            var userList = subscriptionList.Select(sub => sub.User);
            return this.Ok(userList);
        }
    }
}
