using ExampleBusiness.Entities;
using System;
using System.Collections.Generic;

namespace ExampleBusiness.Services
{
    public interface IAuthorsService
    {
        bool AuthorExists(Guid id);

        Author GetAuthorById(Guid id);

        List<Author> GetAuthorList();
    }
}
