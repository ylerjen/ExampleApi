using ExampleDomain.Entities;
using System;
using System.Collections.Generic;

namespace ExampleRepository.Repositories
{
    public interface IAuthorsRepository
    {
        bool AuthorExists(Guid id);

        List<Author> GetAuthorsList();

        Author GetAuthorById(Guid id);

        Author InsertAuthor(Author author);
    }
}
