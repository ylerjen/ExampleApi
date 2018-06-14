using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleRepository.Repositories
{
    public class AuthorsRepository : IAuthorsRepository
    {
        public bool AuthorExists(Guid id)
        {
            return true;
        }
    }
}
