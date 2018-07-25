using Example.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.Api.Contracts
{
    public class BookDto
    {

        public Guid Id { get; set; }

        public string Title { get; set; }

        public Guid AuthorId { get; set; }

        public List<EBookCategory> CategoryList { get; set; }
    }
}
