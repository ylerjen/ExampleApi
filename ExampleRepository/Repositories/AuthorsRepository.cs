using ExampleDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExampleRepository.Repositories
{
    public class AuthorsRepository : IAuthorsRepository
    {

        private readonly List<Author> authorList = new List<Author>() {
            new Author(Guid.NewGuid(), "King", "Stephen"){ Descr = "Master of suspense" },
            new Author(Guid.NewGuid(), "Martin", "George RR"){ Descr = "An American novelist and short-story writer in the fantasy, horror, and science fiction genres" }
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
            this.authorList.Add(author);
            return author;
        }
    }
}
