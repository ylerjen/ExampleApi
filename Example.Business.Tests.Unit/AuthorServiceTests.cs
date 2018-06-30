using Example.Business.Services;
using Example.Business.Tests.Unit.Stubs;
using Example.Repository.Repositories;
using System;
using Example.Domain.Entities;
using Example.Domain.Validations;
using Example.Helpers;
using Xunit;

namespace Example.Business.Tests.Unit
{
    public class AuthorServiceTests
    {
        private IAuthorsRepository AuthorsRepository { get; }
        private IDateTimeProvider DateTimeProvider { get; }

        private IAuthorsService AuthorsService { get; }

        public AuthorServiceTests()
        {
            this.AuthorsRepository = new AuthorsRepositoryStub();
            this.DateTimeProvider = new DateTimeProviderStub();

            this.AuthorsService = new AuthorsService(this.DateTimeProvider, this.AuthorsRepository);
        }

        [Fact]
        public void AuthorExists_should_return_true_if_author_exists()
        {
            var authorSrvc = new AuthorsService(this.DateTimeProvider, this.AuthorsRepository);
            var id = new Guid();

            Assert.True(authorSrvc.AuthorExists(id));
        }

        [Fact]
        public void GetAuthorById_should_return_an_author()
        {
            var id = new Guid();

            Assert.NotNull(this.AuthorsService.GetAuthorById(id));
        }

        [Fact]
        public void CreateAuthor_should_return_an_exception_if_birthdate_is_in_the_futur()
        {
            var futurDate = this.DateTimeProvider.Now().AddYears(1);
            var author = new Author { Birthdate = futurDate };

            Action act = () => this.AuthorsService.CreateAuthor(author);

            Assert.Throws<ValidationException>(act);
        }

        [Fact]
        public void CreateAuthor_should_return_an_author_with_a_created_id()
        {
            var pastDate = this.DateTimeProvider.Now().AddYears(-1);
            var author = new Author { Birthdate = pastDate };

            this.AuthorsService.CreateAuthor(author);

            Assert.NotEqual(Guid.Empty, author.Id);
        }
    }
}