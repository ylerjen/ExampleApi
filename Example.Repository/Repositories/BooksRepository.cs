using System;
using System.Collections.Generic;
using System.Text;
using Example.Domain.Entities;

namespace Example.Repository.Repositories
{
    public class BooksRepository : IBooksRepository
    {

        private readonly List<Book> bookList = new List<Book>() {
            new Book(new Guid("0ec5e125-5eee-4102-b48d-011a600fd74a"), "The Lord of the Rings : The two towers"),
            new Book(new Guid("8f8cfba1-b30e-4289-a7ca-3aa122adb30a"), "Game Of Thrones : Book 1")
        };

        public bool DoesTheBookExist(Guid id)
        {
            return this.bookList.Exists(b => b.Id.Equals(id));
        }

        public Book GetBookById(Guid id)
        {
            return this.bookList.Find(b => b.Id.Equals(id));
        }

        public List<Book> GetBookList()
        {
            return this.bookList;
        }

        public List<Book> GetBookListByAuthor(Guid authorId)
        {
            return this.bookList.FindAll(b => b.AuthorId == authorId);
        }

        public Book InsertBook(Book book)
        {
            book.Id = Guid.NewGuid();
            this.bookList.Add(book);
            return book;
        }
    }
}
