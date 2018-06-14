using System;
using System.Collections.Generic;
using ExampleBusiness.Entities;
using ExampleRepository.Repositories;

namespace ExampleBusiness.Services
{
    public class AuthorsService : IAuthorsService
    {
        private IAuthorsRepository authorsRepository { get; set; }

        private List<Author> authorList = new List<Author>();

        public AuthorsService(/*IAuthorsRepository authorsRepository*/)
        {
            //this.authorsRepository = authorsRepository;
            this.authorList.Add(new Author(new Guid(), "King", "Stephen"));
            this.authorList.Add(new Author(new Guid(), "Martin", "George RR"));
        }

        public bool AuthorExists(Guid id)
        {
            var author = this.authorList.Find(a => a.Id == id);
            return true;
        }

        public Author GetAuthorById(Guid id)
        {
            return this.authorList[0];
        }

        public List<Author> GetAuthorList()
        {
            return this.authorList;
        }
    }
}
