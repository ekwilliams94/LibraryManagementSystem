using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;
using Moq;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Tests.Services
{
    [TestClass]
    public class UserManagementServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UserManagementService _sut;

        public UserManagementServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _sut = new UserManagementService(_userRepositoryMock.Object);
        }

        [TestMethod]
        public void AddUser_AddsUser_WhenValidInputsProvided()
        {
            var user = new User("John", "Doe", "john.doe@example.com");

            _sut.AddUser(user.FirstName, user.LastName, user.Email);

            _userRepositoryMock.Verify(u => u.Add(It.Is<User>( u =>
                u.FirstName == user.FirstName && u.LastName == user.LastName && u.Email == user.Email
            )), Times.Once);
        }

        [TestMethod]
        public void RemoveUser_RemovesUser_WhenUserExists()
        {
            var user = new User("John", "Doe", "john.doe@example.com");

            _userRepositoryMock.Setup(x => x.GetByName(It.IsAny<string>(), It.IsAny<string>())).Returns(user);

            _sut.RemoveUser(user.FirstName, user.LastName);

            _userRepositoryMock.Verify(u => u.Delete(user.Id), Times.Once);
        }

        [TestMethod]
        public void GetUser_ReturnsUser_WhenUserExists()
        {
            var user = new User("John", "Doe", "john.doe@example.com");
            _userRepositoryMock.Setup(u => u.GetByName(user.FirstName, user.LastName)).Returns(user);

            var result = _sut.GetUser(user.FirstName, user.LastName);
            Assert.AreEqual(user, result);
        }

        [TestMethod]
        public void GetUser_ReturnsNull_WhenUserDoesNotExist()
        {
            _userRepositoryMock.Setup(u => u.GetByName(It.IsAny<string>(), It.IsAny<string>())).Returns<User>(null);

            var result = _sut.GetUser("John", "Doe");
            Assert.IsNull(result);
        }
    }
}
