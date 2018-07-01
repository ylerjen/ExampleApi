using Example.Business.Services;
using Example.Repository.Repositories;
using Moq;
using System;
using Example.Domain.Entities;
using Example.Domain.Validations;
using Example.Helpers;
using Xunit;

namespace Example.Business.Tests.Unit
{
    public class AuthorServiceTests
    {
        private Mock<IAuthorsRepository> AuthorsRepositoryMock { get; }
        private Mock<IDateTimeProvider> DateTimeProviderMock { get; }

        private IAuthorsService AuthorsService { get; }
        

        public AuthorServiceTests()
        {
            var fakeNow = new DateTime(2018, 12, 31, 23, 59, 59);
            var fakeAuthor = new Author(Guid.NewGuid(), "Norris", "Chuck", fakeNow);

            var mockRepo = new MockRepository(MockBehavior.Strict);
            this.AuthorsRepositoryMock = mockRepo.Create<IAuthorsRepository>();
            this.DateTimeProviderMock = mockRepo.Create<IDateTimeProvider>();

            this.DateTimeProviderMock
                .Setup(o => o.Now())
                .Returns(fakeNow);

            this.AuthorsRepositoryMock
                .Setup(o => o.InsertAuthor(It.IsAny<Author>()))
                .Returns(fakeAuthor);

            this.AuthorsRepositoryMock
                .Setup(o => o.AuthorExists(It.IsAny<Guid>()))
                .Returns(true);

            this.AuthorsRepositoryMock
                .Setup(o => o.GetAuthorById(It.IsAny<Guid>()))
                .Returns(fakeAuthor);

            this.AuthorsService = new AuthorsService(this.DateTimeProviderMock.Object, this.AuthorsRepositoryMock.Object);
        }

        [Fact]
        public void AuthorExists_should_return_true_if_author_exists()
        {;
            // Arrange
            var id = new Guid();

            // Act
            var authorExist = this.AuthorsService.AuthorExists(id);

            // Assert
            Assert.True(authorExist);
        }

        [Fact]
        public void GetAuthorById_should_return_an_author()
        {
            // Arrange
            var id = new Guid();

            // Act
            var author = this.AuthorsService.GetAuthorById(id);

            // Assert
            Assert.NotNull(author);
        }

        [Fact]
        public void CreateAuthor_should_return_an_exception_if_birthdate_is_in_the_futur()
        {
            // Arrange
            var futurDate = this.DateTimeProviderMock.Object.Now().AddYears(1);
            var author = new Author { Birthdate = futurDate };

            // Act
            Action act = () => this.AuthorsService.CreateAuthor(author);

            // Assert
            Assert.Throws<ValidationException>(act);
        }

        [Fact]
        public void CreateAuthor_should_return_an_author_with_a_created_id()
        {
            // Arrange
            var pastDate = this.DateTimeProviderMock.Object.Now().AddYears(-1);
            var author = new Author { Birthdate = pastDate };

            this.AuthorsRepositoryMock
                .Setup(o => o.InsertAuthor(It.IsAny<Author>()))
                .Returns(() =>
                {
                    author.Id = Guid.NewGuid();
                    return author;
                });

            // Act
            this.AuthorsService.CreateAuthor(author);

            // Assert
            Assert.NotEqual(Guid.Empty, author.Id);
        }
    }
}