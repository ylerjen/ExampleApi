using Example.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Example.Repository.Repositories
{
    public class AuthorsRepository : IAuthorsRepository
    {
        private readonly List<Author> authorList = new List<Author>() {
            new Author(new Guid("0ec5e125-5eee-4102-b48d-011a600fd74a"), "King", "Stephen", new DateTime()){ Descr = "Master of suspense" },
            new Author(new Guid("8f8cfba1-b30e-4289-a7ca-3aa122adb30a"), "Martin", "George RR", new DateTime()){ Descr = "An American novelist and short-story writer in the fantasy, horror, and science fiction genres" }
        };

        public List<Author> GetAuthorsList()
        {
            return this.authorList;
        }

        public Author GetAuthorById(Guid id)
        {
            if (id == null)
            {
                return null;
            }
            return this.authorList.Find(a => a.Id == id);
        }

        public bool AuthorExists(Guid id)
        {
            if (id == null)
            {
                return false;
            }
            return this.authorList.Any(a => a.Id == id);
        }

        public Author InsertAuthor(Author author)
        {
            author.Id = Guid.NewGuid();
            authorList.Add(author);
            return author;
        }
    }
}
