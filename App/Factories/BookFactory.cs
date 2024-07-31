using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Factories
{
    public interface IBookFactory
    {
        Book CreateBook(string bookType, string title, string author, string isbn);
    }

    internal class BookFactory : IBookFactory
    {
        public Book CreateBook(string bookType, string title, string author, string isbn)
        {
            if (string.IsNullOrWhiteSpace(bookType))
                throw new ArgumentNullException(nameof(bookType));
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException(nameof(title));
            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentNullException(nameof(author));
            if (string.IsNullOrWhiteSpace(isbn))
                throw new ArgumentNullException(nameof(isbn));

            return bookType.ToLowerInvariant() switch
            {
                "fiction" => new FictionBook
                {
                    Id = Guid.NewGuid(),
                    Title = title,
                    Author = author,
                    Availability = BookAvailability.Available,
                    ISBN = isbn
                },
                "non-fiction" => new NonfictionBook
                {
                    Id = Guid.NewGuid(),
                    Title = title,
                    Author = author,
                    Availability = BookAvailability.Available,
                    ISBN = isbn
                },
                _ => throw new ArgumentException("Invalid book type. Must be 'fiction' or 'non-fiction'."),
            };
        }
    }
}
