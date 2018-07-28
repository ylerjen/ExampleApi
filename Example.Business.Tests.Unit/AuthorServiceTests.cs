using Example.Business.Services;
using Example.Repository.Repositories;
using Moq;
using System;
using Example.Domain.Entities;
using Example.Domain.Exceptions;
using Example.Domain.Validations;
using Example.Helpers;
using Xunit;

namespace Example.Business.Tests.Unit
{
    public class UserServiceTests
    {
        private Mock<IUsersRepository> UsersRepositoryMock { get; }
        private Mock<IDateTimeProvider> DateTimeProviderMock { get; }

        private IUsersService UsersService { get; }
        

        public UserServiceTests()
        {
            var fakeNow = new DateTime(2018, 12, 31, 23, 59, 59);
            var fakeUser = new User(Guid.NewGuid(), "Norris", "Chuck", fakeNow);

            var mockRepo = new MockRepository(MockBehavior.Strict);
            this.UsersRepositoryMock = mockRepo.Create<IUsersRepository>();
            this.DateTimeProviderMock = mockRepo.Create<IDateTimeProvider>();

            this.DateTimeProviderMock
                .Setup(o => o.Now())
                .Returns(fakeNow);

            this.UsersRepositoryMock
                .Setup(o => o.InsertUser(It.IsAny<User>()))
                .Returns(fakeUser);

            this.UsersRepositoryMock
                .Setup(o => o.UserExists(It.IsAny<Guid>()))
                .Returns(true);

            this.UsersRepositoryMock
                .Setup(o => o.GetUserById(It.IsAny<Guid>()))
                .Returns(fakeUser);

            this.UsersService = new UsersService(this.DateTimeProviderMock.Object, this.UsersRepositoryMock.Object);
        }

        [Fact]
        public void UserExists_should_return_true_if_User_exists()
        {;
            // Arrange
            var id = new Guid();

            // Act
            var userExist = this.UsersService.UserExists(id);

            // Assert
            Assert.True(userExist);
        }

        [Fact]
        public void GetUserById_should_return_an_User()
        {
            // Arrange
            var id = new Guid();

            // Act
            var user = this.UsersService.GetUserById(id);

            // Assert
            Assert.NotNull(user);
        }

        [Fact]
        public void CreateUser_should_return_an_exception_if_birthdate_is_in_the_futur()
        {
            // Arrange
            var futurDate = this.DateTimeProviderMock.Object.Now().AddYears(1);
            var user = new User { Birthdate = futurDate };

            // Act
            Action act = () => this.UsersService.CreateUser(user);

            // Assert
            Assert.Throws<ValidationException>(act);
        }

        [Fact]
        public void CreateUser_should_return_an_User_with_a_created_id()
        {
            // Arrange
            var pastDate = this.DateTimeProviderMock.Object.Now().AddYears(-1);
            var user = new User { Birthdate = pastDate };

            this.UsersRepositoryMock
                .Setup(o => o.InsertUser(It.IsAny<User>()))
                .Returns(() =>
                {
                    user.Id = Guid.NewGuid();
                    return user;
                });

            // Act
            this.UsersService.CreateUser(user);

            // Assert
            Assert.NotEqual(Guid.Empty, user.Id);
        }
    }
}