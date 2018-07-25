using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Domain.Entities
{
    public class Book
    {
        public Book()
        {

        }

        public Book(Guid id, string title)
        {
            this.Id = id;
            this.Title = title;
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public Guid AuthorId { get; set; }

        public List<EBookCategory> CategoryList { get; set; }
    }
}
