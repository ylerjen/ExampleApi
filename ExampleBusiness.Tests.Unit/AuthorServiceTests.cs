using ExampleBusiness.Services;
using ExampleBusiness.Tests.Unit.Stubs;
using ExampleRepository.Repositories;
using System;
using Xunit;

namespace ExampleBusiness.Tests.Unit
{
    public class AuthorServiceTests
    {
        private IAuthorsRepository authorsRepository { get; set; }

        public AuthorServiceTests()
        {
            this.authorsRepository = new AuthorsRepositoryStub();
        }

        [Fact]
        public void AuthorExists_should_return_true_if_author_exists()
        {
            var authorSrvc = new AuthorsService(this.authorsRepository);
            var id = new Guid();

            Assert.True(authorSrvc.AuthorExists(id));
        }

        [Fact]
        public void GetAuthorById_should_return_an_author()
        {
            var authorSrvc = new AuthorsService(this.authorsRepository);
            var id = new Guid();

            Assert.NotNull(authorSrvc.GetAuthorById(id));
        }
    }
}
