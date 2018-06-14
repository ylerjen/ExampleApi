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
    }
}
