using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleRepository.Repositories
{
    public interface IAuthorsRepository
    {
        bool AuthorExists(Guid id);
    }
}
