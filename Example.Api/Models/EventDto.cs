using Example.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Example.Api.Models
{
    public class EventDto
    {

        public Guid Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Title { get; set; }

        public Guid AuthorId { get; set; }

        public List<EventCategory> CategoryList { get; set; }
    }
}
