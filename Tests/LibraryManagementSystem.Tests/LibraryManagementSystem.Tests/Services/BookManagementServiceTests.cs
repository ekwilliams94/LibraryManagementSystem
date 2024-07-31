using LibraryManagementSystem.Models;
using Moq;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Services;
using LibraryManagementSystem.Factories;

namespace LibraryManagementSystem.Tests.Services
{
    [TestClass]
    public class BookManagementServiceTests
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly Mock<IBookFactory> _bookFactoryMock;
        private readonly IBookManagementService _sut;

        public BookManagementServiceTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _bookFactoryMock = new Mock<IBookFactory>();
            _sut = new BookManagementService(_bookFactoryMock.Object, _bookRepositoryMock.Object);
        }

        [TestMethod]
        public void AddBook_AddsBook_WhenValidInputsProvided()
        {
            var book = new FictionBook
            {
                Id = Guid.NewGuid(),
                Title = "Test Title",
                Author = "Test Author",
                ISBN = "1234567890",
                Availability = BookAvailability.Available
            };

            _bookFactoryMock.Setup(x => x.CreateBook(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(book);

            _sut.AddBook("Fiction", "Test Title", "Test Author", "1234567890");

            _bookRepositoryMock.Verify(b => b.Add(book), Times.Once);
        }


        [TestMethod]
        public void RemoveBook_RemovesBook_WhenBookExists()
        {
            var book = new FictionBook
            {
                Id = Guid.NewGuid(),
                Title = "Test Title",
                Author = "Test Author",
                ISBN = "1234567890",
                Availability = BookAvailability.Available
            };

            _bookRepositoryMock.Setup(x => x.GetByISBN(It.IsAny<string>())).Returns(book);

            _sut.RemoveBook(book.ISBN);

            _bookRepositoryMock.Verify(b => b.Delete(book.Id), Times.Once);
        }

        [TestMethod]
        public void GetBook_ReturnsBook_WhenBookExists()
        {
            var book = new FictionBook
            {
                Id = Guid.NewGuid(),
                Title = "Test Title",
                Author = "Test Author",
                ISBN = "1234567890",
                Availability = BookAvailability.Available
            };
            _bookRepositoryMock.Setup(b => b.GetByISBN(book.ISBN)).Returns(book);

            var result = _sut.GetBook(book.ISBN);
            Assert.AreEqual(book, result);
        }

        [TestMethod]
        public void GetBook_ReturnsNull_WhenBookDoesNotExist()
        {
            _bookRepositoryMock.Setup(b => b.GetByISBN(It.IsAny<string>())).Returns<Book>(null);

            var result = _sut.GetBook("1234567890");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void LoanBook_LoansBook_WhenBookAvailable()
        {
            var book = new FictionBook
            {
                Id = Guid.NewGuid(),
                Title = "Test Title",
                Author = "Test Author",
                ISBN = "1234567890",
                Availability = BookAvailability.Available
            };

            _bookRepositoryMock.Setup(b => b.GetByISBN(book.ISBN)).Returns(book);

            _sut.LoanBook(book);

            _bookRepositoryMock.Verify(b => b.Update(It.Is<Book>(bk => bk.Availability == BookAvailability.Unavailable)), Times.Once);
        }

        [TestMethod]
        public void ReturnBook_ReturnsBook()
        {
            var book = new FictionBook
            {
                Id = Guid.NewGuid(),
                Title = "Test Title",
                Author = "Test Author",
                ISBN = "1234567890",
                Availability = BookAvailability.Unavailable
            };

            _bookRepositoryMock.Setup(b => b.GetByISBN(book.ISBN)).Returns(book);

            _sut.ReturnBook(book);

            _bookRepositoryMock.Verify(b => b.Update(It.Is<Book>(bk => bk.Availability == BookAvailability.Available)), Times.Once);
        }
    }
}
