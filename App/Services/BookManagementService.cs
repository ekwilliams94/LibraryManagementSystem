using LibraryManagementSystem.Factories;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Services
{
    public interface IBookManagementService {
        void AddBook(string bookType, string title, string author, string isbn);
        void RemoveBook(string isbn);
        Book? GetBook(string isbn);
        IEnumerable<Book> GetBooks();
        void LoanBook(Book book);
        void ReturnBook(Book book);

    }

    public class BookManagementService : IBookManagementService
    {
        private readonly IBookFactory _bookFactory;
        private readonly IBookRepository _bookRepository;
        public BookManagementService(IBookFactory bookFactory, IBookRepository bookRepository) { 
            _bookFactory = bookFactory;
            _bookRepository = bookRepository;
        }

        public void AddBook(string bookType, string title, string author, string isbn)
        {
            var book = _bookFactory.CreateBook(bookType, title, author, isbn);
            _bookRepository.Add(book);
        }

        public Book? GetBook(string isbn)
        {
            return _bookRepository.GetByISBN(isbn);
        }

        public IEnumerable<Book> GetBooks()
        {
            return _bookRepository.GetAll();
        }

        public void LoanBook(Book book)
        {
            if (book.Availability == BookAvailability.Unavailable)
            {
                throw new Exception("This book is already on loan");
            }

            book.Availability = BookAvailability.Unavailable;
            _bookRepository.Update(book);
        }

        public void RemoveBook(string isbn)
        {
            var book = _bookRepository.GetByISBN(isbn);

            if(book != null)
            {
                _bookRepository.Delete(book.Id);
            }
        }

        public void ReturnBook(Book book)
        {
            book.Availability = BookAvailability.Available;
            _bookRepository.Update(book);
        }
    }
}
