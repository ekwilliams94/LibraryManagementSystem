using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Tests.Repositories
{
    [TestClass]
    public class UserRepositoryTests
    {
        private readonly IUserRepository _sut = new UserRepository();

        [TestMethod]
        public void AddUser_AddsUser_WhenValidUserProvided()
        {
            var user = new User("John", "Doe", "john.doe@example.com");

            _sut.Add(user);

            var result = _sut.GetById(user.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(user.Id, result.Id);
            Assert.AreEqual(user.FirstName, result.FirstName);
            Assert.AreEqual(user.LastName, result.LastName);
            Assert.AreEqual(user.Email, result.Email);
        }

        [TestMethod]
        public void AddUser_ThrowsException_WhenUserWithSameNameAlreadyExists()
        {
            var user = new User("John", "Doe", "john.doe@example.com");
            _sut.Add(user);

            var ex = Assert.ThrowsException<InvalidOperationException>(() => _sut.Add(user));
            Assert.AreEqual("This user already exists", ex.Message);
        }

        [TestMethod]
        public void GetById_ReturnsUser_WhenUserExists()
        {
            var user = new User("John", "Doe", "john.doe@example.com");
            _sut.Add(user);

            var result = _sut.GetById(user.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(user.Id, result.Id);
            Assert.AreEqual(user.FirstName, result.FirstName);
            Assert.AreEqual(user.LastName, result.LastName);
        }

        [TestMethod]
        public void GetById_ReturnsNull_WhenUserDoesNotExist()
        {
            var result = _sut.GetById(Guid.NewGuid());
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetByName_ReturnsUser_WhenUserExists()
        {
            var user = new User("John", "Doe", "john.doe@example.com");
            _sut.Add(user);

            var result = _sut.GetByName("John", "Doe");
            Assert.IsNotNull(result);
            Assert.AreEqual(user.FirstName, result.FirstName);
            Assert.AreEqual(user.LastName, result.LastName);
        }

        [TestMethod]
        public void GetByName_ReturnsNull_WhenUserDoesNotExist()
        {
            var result = _sut.GetByName("John", "Doe");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void UpdateUser_UpdatesUser_WhenUserExists()
        {
            var user = new User("John", "Doe", "john.doe@example.com");
            _sut.Add(user);

            var updatedUser = new User("John", "Smith", "john.smith@example.com")
            {
                Id = user.Id
            };
            _sut.Update(updatedUser);

            var result = _sut.GetById(user.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(updatedUser.FirstName, result.FirstName);
            Assert.AreEqual(updatedUser.LastName, result.LastName);
            Assert.AreEqual(updatedUser.Email, result.Email);
        }

        [TestMethod]
        public void UpdateUser_ThrowsException_WhenUserDoesNotExist()
        {
            var user = new User("John", "Doe", "john.doe@example.com");

            var ex = Assert.ThrowsException<Exception>(() => _sut.Update(user));
            Assert.AreEqual("User does not exist within repository", ex.Message);
        }

        [TestMethod]
        public void DeleteUser_RemovesUser_WhenUserExists()
        {
            var user = new User("John", "Doe", "john.doe@example.com");
            _sut.Add(user);

            _sut.Delete(user.Id);

            var result = _sut.GetById(user.Id);
            Assert.IsNull(result);
        }
    }
}
