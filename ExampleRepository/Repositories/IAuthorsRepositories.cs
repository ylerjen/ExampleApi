using ExampleDomain.Entities;
using System;
using System.Collections.Generic;

namespace ExampleRepository.Repositories
{
    public interface IAuthorsRepository
    {
        List<Author> GetAuthorsList();

        Author GetAuthorById(Guid id);

        bool AuthorExists(Guid id);
    }
}
