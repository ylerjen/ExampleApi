using System;
using System.Collections.Generic;
using System.Linq;
using Example.Domain.Entities;
using Example.Domain.Rules.Authors;
using Example.Domain.Validations;
using Example.Helpers;
using Example.Repository.Repositories;
using NRules;
using NRules.Fluent;

namespace Example.Business.Services
{
    public class AuthorsService : RuleService, IAuthorsService
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
            //Insert facts into rules engine's memory
            this.RulesSession.Insert(author);
            this.RunRulesSession(true);

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
