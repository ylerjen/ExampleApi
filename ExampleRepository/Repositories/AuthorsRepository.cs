using ExampleDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExampleRepository.Repositories
{
    public class AuthorsRepository : IAuthorsRepository
    {

        private readonly List<Author> authorList = new List<Author>() {
            new Author(new Guid(), "King", "Stephen"),
            new Author(new Guid(), "Martin", "George RR")
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
            return true;
        }
    }
}
