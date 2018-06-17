using ExampleDomain.Entities;
using ExampleRepository.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleBusiness.Tests.Unit.Stubs
{
    public class AuthorsRepositoryStub : IAuthorsRepository
    {
        public bool AuthorExists(Guid id)
        {
            return true;
        }

        public Author GetAuthorById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Author> GetAuthorsList()
        {
            throw new NotImplementedException();
        }
    }
}
