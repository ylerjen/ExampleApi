using System;
using System.Collections.Generic;
using ExampleDomain.Entities;
using ExampleDomain.Validations;
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

        public Author CreateAuthor(Author author)
        {
            if (author.Birthdate > DateTime.Now)
            {
                throw new ValidationException("birthdate should be in the future", nameof(author.Birthdate));
            }
            return this.authorsRepository.InsertAuthor(author);
        }

        public void DeleteAuthor(Guid id)
        {
            throw new NotImplementedException();
        }

        public void UpdateAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public void ModifyAuthor(string action, string prop, object newValue)
        {
            throw new NotImplementedException();
        }
    }
}
