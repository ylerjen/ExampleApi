using System;
using System.Collections.Generic;
using Example.Api.Controllers;
using Example.Business.Services;
using Example.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
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
        public void GetAuthor_should_return_a_200OK_if_everything_goes_well()
        {
            // Arrange
            this.authorsServiceMock
                .Setup(m => m.GetAuthorsList())
                .Returns(new List<Author>());
            var authorsCtrlr = new AuthorsController(this.authorsServiceMock.Object);

            // Act
            var result = authorsCtrlr.GetAuthorList();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetAuthor_should_return_a_BadRequest_if_Guid_is_invalid()
        {
            // Arrange
            this.authorsServiceMock
                .Setup(m => m.GetAuthorsList())  // will set up CommandBase.Execute
                .Returns(new List<Author>());
            var authorsCtrlr = new AuthorsController(this.authorsServiceMock.Object);

            // Act
            var result = authorsCtrlr.GetAuthor(Guid.Empty);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
