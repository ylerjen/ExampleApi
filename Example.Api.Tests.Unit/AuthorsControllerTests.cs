using System;
using System.Collections.Generic;
using Example.Business.Services;
using Example.Domain.Entities;
using ExampleApi.Controllers;
using Moq;
using Xunit;

namespace Example.Api.Tests.Unit
{
    public class AuthorsControllerTests
    {
        private Mock<IAuthorsService> authorsServiceMock { get; set; }

        public AuthorsControllerTests()
        {
            var repository = new MockRepository(MockBehavior.Strict) { DefaultValue = DefaultValue.Mock };

            // Create a mock using the repository settings
            this.authorsServiceMock = repository.Create<IAuthorsService>();
        }

        [Fact]
        public void GetAuthor_should_be_callable()
        {
            // Arrange
            this.authorsServiceMock
                .Setup(m => m.GetAuthorsList())  // will set up CommandBase.Execute
                .Returns(new List<Author>());
            var authorsCtrlr = new AuthorsController(this.authorsServiceMock.Object);

            // Act
            var resp = authorsCtrlr.GetAuthorList();

            // Assert
            Assert.Equal("", "");
        }
    }
}
