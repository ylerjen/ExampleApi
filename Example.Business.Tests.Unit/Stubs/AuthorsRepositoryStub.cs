using Example.Domain.Entities;
using Example.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Business.Tests.Unit.Stubs
{
    public class AuthorsRepositoryStub : IAuthorsRepository
    {
        public bool AuthorExists(Guid id)
        {
            return true;
        }

        public Author GetAuthorById(Guid id)
        {
            return new Author() { Id = id, Lastname = "King", Firstname = "Stephen" };
        }

        public List<Author> GetAuthorsList()
        {
            throw new NotImplementedException();
        }

        public Author InsertAuthor(Author author)
        {
            throw new NotImplementedException();
        }
    }
}
