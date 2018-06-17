using System;
using System.Collections.Generic;
using ExampleDomain.Entities;
using ExampleRepository.Repositories;

namespace ExampleBusiness.Services
{
    public class AuthorsService : IAuthorsService
    {
        private IAuthorsRepository authorsRepository { get; set; }

        public AuthorsService(IAuthorsRepository authorsRepository)
        {
            this.authorsRepository = authorsRepository;
        }

        public bool AuthorExists(Guid id)
        {
            return this.authorsRepository.AuthorExists(id);
        }

        public Author GetAuthorById(Guid id)
        {
            return this.authorsRepository.GetAuthorById(id);
        }

        public List<Author> GetAuthorsList()
        {
            return this.authorsRepository.GetAuthorsList();
        }
    }
}
