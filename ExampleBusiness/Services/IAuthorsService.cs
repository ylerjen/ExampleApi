using ExampleDomain.Entities;
using System;
using System.Collections.Generic;

namespace ExampleBusiness.Services
{
    public interface IAuthorsService
    {
        bool AuthorExists(Guid id);

        Author GetAuthorById(Guid id);

        List<Author> GetAuthorsList();

        Author CreateAuthor(Author author);

        void DeleteAuthor(Guid id);

        void UpdateAuthor(Author author);

        void ModifyAuthor(string action, string prop, object newValue);
    }
}
