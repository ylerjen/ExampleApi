using Example.Business.Services;
using Example.Repository.Repositories;
using Moq;
using System;
using Xunit;

namespace Example.Business.Tests.Unit
{
    public class AuthorServiceTests
    {
        private Mock<IAuthorsRepository> AuthorsRepositoryMock { get; set; }

        public AuthorServiceTests()
        {
            var mockRepo = new MockRepository(MockBehavior.Strict);
            this.AuthorsRepositoryMock = mockRepo.Create<IAuthorsRepository>();
        }

        [Fact]
        public void AuthorExists_should_return_true_if_author_exists()
        {
            var authorSrvc = new AuthorsService(this.AuthorsRepositoryMock.Object);
            var id = new Guid();

            Assert.True(authorSrvc.AuthorExists(id));
        }

        [Fact]
        public void GetAuthorById_should_return_an_author()
        {
            var authorSrvc = new AuthorsService(this.AuthorsRepositoryMock.Object);
            var id = new Guid();

            Assert.NotNull(authorSrvc.GetAuthorById(id));
        }
    }
}
