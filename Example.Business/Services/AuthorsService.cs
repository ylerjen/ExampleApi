using System;
using System.Collections.Generic;
using Example.Domain.Entities;
using Example.Domain.Validations;
using Example.Helpers;
using Example.Repository.Repositories;
using Microsoft.AspNetCore.Authentication;

namespace Example.Business.Services
{
    public class AuthorsService : IAuthorsService
    {
        private IAuthorsRepository AuthorsRepository { get; }

        private IDateTimeProvider DateTimeProvider { get; }

        public AuthorsService(IDateTimeProvider dateTimeProvider, IAuthorsRepository authorsRepository)
        {
            this.DateTimeProvider = dateTimeProvider;
            this.AuthorsRepository = authorsRepository;
        }

        public bool AuthorExists(Guid id)
        {
            return this.AuthorsRepository.AuthorExists(id);
        }

        public Author GetAuthorById(Guid id)
        {
            return this.AuthorsRepository.GetAuthorById(id);
        }

        public List<Author> GetAuthorsList()
        {
            return this.AuthorsRepository.GetAuthorsList();
        }

        public Author CreateAuthor(Author author)
        {
            if (author.Birthdate > this.DateTimeProvider.Now())
            {
                throw new ValidationException("birthdate should be in the future", nameof(author.Birthdate));
            }
            return this.AuthorsRepository.InsertAuthor(author);
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
