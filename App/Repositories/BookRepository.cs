using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Book? GetByISBN(string isbn); 
    }

    public class BookRepository : IBookRepository
    {
        private readonly IList<Book> _books = new List<Book>(); 

        public void Add(Book entity)
        {
            if (_books.Any(b => b.ISBN == entity.ISBN))
                throw new InvalidOperationException("This book already exists");

            _books.Add(entity);
        }

        public void Delete(Guid id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book != null)
                _books.Remove(book);
        }

        public IEnumerable<Book> GetAll() => _books;

        public Book? GetById(Guid id) => _books.FirstOrDefault(b => b.Id == id);

        public Book? GetByISBN(string isbn) => _books.FirstOrDefault(b => b.ISBN == isbn);

        public void Update(Book entity)
        {
            var book = _books.FirstOrDefault(b => b.Id == entity.Id)
                ?? throw new Exception("Book does not exist within repository");

            book.ISBN = entity.ISBN;
            book.Title = entity.Title;
            book.Author = entity.Author;
            book.Availability = entity.Availability;
        }
    }
}
