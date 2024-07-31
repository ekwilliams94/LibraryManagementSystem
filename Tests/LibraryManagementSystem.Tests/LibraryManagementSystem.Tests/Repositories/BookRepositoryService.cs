using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Tests.Repositories
{
    [TestClass]
    public class BookRepositoryTests
    {
        private readonly IBookRepository _sut = new BookRepository();

        [TestMethod]
        public void AddBook_AddsBook_WhenValidBookProvided()
        {
            var book = new FictionBook
            {
                Id = Guid.NewGuid(),
                Title = "Test Title",
                Author = "Test Author",
                ISBN = "1234567890",
                Availability = BookAvailability.Available
            };

            _sut.Add(book);

            var result = _sut.GetById(book.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(book.Id, result.Id);
            Assert.AreEqual(book.Title, result.Title);
            Assert.AreEqual(book.Author, result.Author);
            Assert.AreEqual(book.ISBN, result.ISBN);
            Assert.AreEqual(book.Availability, result.Availability);
        }

        [TestMethod]
        public void AddBook_ThrowsException_WhenBookWithSameISBNAlreadyExists()
        {
            var book = new FictionBook
            {
                Id = Guid.NewGuid(),
                Title = "Test Title",
                Author = "Test Author",
                ISBN = "1234567890",
                Availability = BookAvailability.Available
            };
            _sut.Add(book);

            var ex = Assert.ThrowsException<InvalidOperationException>(() => _sut.Add(book));
            Assert.AreEqual("This book already exists", ex.Message);
        }

        [TestMethod]
        public void GetById_ReturnsBook_WhenBookExists()
        {
            var book = new FictionBook
            {
                Id = Guid.NewGuid(),
                Title = "Test Title",
                Author = "Test Author",
                ISBN = "1234567890",
                Availability = BookAvailability.Available
            };
            _sut.Add(book);

            var result = _sut.GetById(book.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(book.Id, result.Id);
            Assert.AreEqual(book.Title, result.Title);
            Assert.AreEqual(book.Author, result.Author);
        }

        [TestMethod]
        public void GetById_ReturnsNull_WhenBookDoesNotExist()
        {
            var result = _sut.GetById(Guid.NewGuid());
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetByISBN_ReturnsBook_WhenBookExists()
        {
            var book = new FictionBook
            {
                Id = Guid.NewGuid(),
                Title = "Test Title",
                Author = "Test Author",
                ISBN = "1234567890",
                Availability = BookAvailability.Available
            };
            _sut.Add(book);

            var result = _sut.GetByISBN("1234567890");
            Assert.IsNotNull(result);
            Assert.AreEqual(book.ISBN, result.ISBN);
        }

        [TestMethod]
        public void GetByISBN_ReturnsNull_WhenBookDoesNotExist()
        {
            var result = _sut.GetByISBN("1234567890");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void UpdateBook_UpdatesBook_WhenBookExists()
        {
            var book = new FictionBook
            {
                Id = Guid.NewGuid(),
                Title = "Test Title",
                Author = "Test Author",
                ISBN = "1234567890",
                Availability = BookAvailability.Available
            };
            _sut.Add(book);

            var updatedBook = new FictionBook
            {
                Id = book.Id,
                Title = "Updated Title",
                Author = "Updated Author",
                ISBN = book.ISBN,
                Availability = BookAvailability.Available
            };
            _sut.Update(updatedBook);

            var result = _sut.GetById(book.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(updatedBook.Title, result.Title);
            Assert.AreEqual(updatedBook.Author, result.Author);
            Assert.AreEqual(updatedBook.Availability, result.Availability);
        }

        [TestMethod]
        public void UpdateBook_ThrowsException_WhenBookDoesNotExist()
        {
            var book = new FictionBook
            {
                Id = Guid.NewGuid(),
                Title = "Test Title",
                Author = "Test Author",
                ISBN = "1234567890",
                Availability = BookAvailability.Available
            };

            var ex = Assert.ThrowsException<Exception>(() => _sut.Update(book));
            Assert.AreEqual("Book does not exist within repository", ex.Message);
        }

        [TestMethod]
        public void DeleteBook_RemovesBook_WhenBookExists()
        {
            var book = new FictionBook
            {
                Id = Guid.NewGuid(),
                Title = "Test Title",
                Author = "Test Author",
                ISBN = "1234567890",
                Availability = BookAvailability.Available
            };
            _sut.Add(book);

            _sut.Delete(book.Id);

            var result = _sut.GetById(book.Id);
            Assert.IsNull(result);
        }
    }
}
