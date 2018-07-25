using Example.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Repository.Repositories
{
    public interface IBooksRepository
    {
        bool DoesTheBookExist(Guid id);

        List<Book> GetBookList();

        Book GetBookById(Guid id);

        Book InsertBook(Book author);

        List<Book> GetBookListByAuthor(Guid authorId);
    }
}
