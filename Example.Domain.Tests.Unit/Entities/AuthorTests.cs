using System;
using Example.Domain.Entities;
using Xunit;

namespace Example.Domain.Tests.Unit
{
    public class AuthorTests
    {
        [Fact]
        public void AuthorCtor_should_construct_the_object_correctly()
        {
            var author = new Author();

            Assert.Equal(Guid.Empty, author.Id);
            Assert.Null(author.Lastname);
            Assert.Null(author.Firstname);
            Assert.Equal(string.Empty, author.Descr);
            Assert.Empty(author.Descr);
        }

        [Fact]
        public void AuthorCtor_should_construct_the_object_correctly_with_passed_param()
        {
            var id = Guid.NewGuid();
            const string lname = "Norris";
            const string fname = "Chuck";
            var birthdate = new DateTime();

            var author = new Author(id, lname, fname, birthdate);

            Assert.Equal(id, author.Id);
            Assert.Equal(lname, author.Lastname);
            Assert.Equal(fname, author.Firstname);
            Assert.Empty(author.Descr);
        }
    }
}
