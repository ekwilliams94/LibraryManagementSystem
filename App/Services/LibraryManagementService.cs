using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

public sealed class LibraryManagementService
{
    private static LibraryManagementService? _instance;
    private static readonly object _lock = new();
    private readonly IBookManagementService _bookManagementService;
    private readonly IUserManagementService _userManagementService;
    private const string NO_BOOK_TITLE = "Need to supply a book title";
    private const string NO_AUTHOR = "Need to supply an author";
    private const string NO_ISBN = "Need to supply an ISBN";
    private const string NO_BOOK_TYPE = "Need to supply a book type, fiction or non-fiction";
    private const string NO_FIRST_NAME = "Need to supply a first name";
    private const string NO_LAST_NAME = "Need to supply a last name";

    private LibraryManagementService(IBookManagementService bookManagementService, IUserManagementService userManagementService)
    {
        _bookManagementService = bookManagementService;
        _userManagementService = userManagementService;
    }

    public static LibraryManagementService Instance(IBookManagementService bookManagementService, IUserManagementService userManagementService)
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new LibraryManagementService(bookManagementService, userManagementService);
                }
            }
        }
        return _instance;
    }

    public void DisplayOptions()
    {
        while (true)
        {
            Console.WriteLine("\nLibrary Management System:");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Remove Book");
            Console.WriteLine("3. Loan Book");
            Console.WriteLine("4. Return Book");
            Console.WriteLine("5. Get Book Details");
            Console.WriteLine("6. Add User");
            Console.WriteLine("7. Remove User");
            Console.WriteLine("8. Get User Details");
            Console.WriteLine("9. Exit");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddBook();
                    break;
                case "2":
                    RemoveBook();
                    break;
                case "3":
                    LoanBook();
                    break;
                case "4":
                    ReturnBook();
                    break;
                case "5":
                    GetBookDetails();
                    break;
                case "6":
                    AddUser();
                    break;
                case "7":
                    RemoveUser();
                    break;
                case "8":
                    GetUserDetails();
                    break;
                case "9":
                    Console.WriteLine("Ending the application");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Enter a number from 1 to 9");
                    break;
            }
        }
    }

    public void AddBook()
    {
        Console.WriteLine("Enter Book Title:");
        var title = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine(NO_BOOK_TITLE);
            return;
        }

        Console.WriteLine("Enter Author:");
        var author = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(author))
        {
            Console.WriteLine(NO_AUTHOR);
            return;
        }

        Console.WriteLine("Enter ISBN:");
        var isbn = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(isbn))
        {
            Console.WriteLine(NO_ISBN);
            return;
        }

        Console.WriteLine("Enter book type: fiction or non-fiction");
        var bookType = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(bookType))
        {
            Console.WriteLine(NO_BOOK_TYPE);
            return;
        }

        try
        {
            _bookManagementService.AddBook(bookType, title, author, isbn);
            Console.WriteLine("Book added successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void RemoveBook()
    {
        Console.WriteLine("Enter ISBN:");
        var isbn = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(isbn))
        {
            Console.WriteLine(NO_ISBN);
            return;
        }

        try
        {
            _bookManagementService.RemoveBook(isbn);
            Console.WriteLine("Book has been removed");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void GetBookDetails()
    {
        Console.WriteLine("Enter ISBN:");
        var isbn = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(isbn))
        {
            Console.WriteLine(NO_ISBN);
            return;
        }

        try
        {
            var book = _bookManagementService.GetBook(isbn);

            if (book == null)
            {
                Console.WriteLine("No book found with that ISBN");
                return;
            }

            book.DisplayBookInfo();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void AddUser()
    {
        Console.WriteLine("Enter First Name:");
        var firstName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(firstName))
        {
            Console.WriteLine(NO_FIRST_NAME);
            return;
        }

        Console.WriteLine("Enter Last Name:");
        var lastName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(lastName))
        {
            Console.WriteLine(NO_LAST_NAME);
            return;
        }

        Console.WriteLine("Enter Email:");
        var email = Console.ReadLine();

        try
        {
            _userManagementService.AddUser(firstName, lastName, email);
            Console.WriteLine("User added successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void RemoveUser()
    {
        Console.WriteLine("Enter First Name:");
        var firstName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(firstName))
        {
            Console.WriteLine(NO_FIRST_NAME);
            return;
        }

        Console.WriteLine("Enter Last Name:");
        var lastName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(lastName))
        {
            Console.WriteLine(NO_LAST_NAME);
            return;
        }

        try
        {
            _userManagementService.RemoveUser(firstName, lastName);
            Console.WriteLine("User has been removed");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void GetUserDetails()
    {
        Console.WriteLine("Enter First Name:");
        var firstName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(firstName))
        {
            Console.WriteLine(NO_FIRST_NAME);
            return;
        }

        Console.WriteLine("Enter Last Name:");
        var lastName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(lastName))
        {
            Console.WriteLine(NO_LAST_NAME);
            return;
        }

        try
        {
            var user = _userManagementService.GetUser(firstName, lastName);

            if (user == null)
            {
                Console.WriteLine("User does not exist in this system");
                return;
            }

            user.DisplayUserInfo();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void LoanBook()
    {
        Console.WriteLine("Enter ISBN:");
        var isbn = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(isbn))
        {
            Console.WriteLine(NO_ISBN);
            return;
        }

        Console.WriteLine("Enter First Name of user requesting book:");
        var firstName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(firstName))
        {
            Console.WriteLine(NO_FIRST_NAME);
            return;
        }

        Console.WriteLine("Enter Last Name of user requesting book:");
        var lastName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(lastName))
        {
            Console.WriteLine(NO_LAST_NAME);
            return;
        }

        try
        {
            var book = _bookManagementService.GetBook(isbn);

            if (book == null)
            {
                Console.WriteLine("No book found with that ISBN");
                return;
            }

            var user = _userManagementService.GetUser(firstName, lastName);

            if (user == null)
            {
                Console.WriteLine("No user found with that name in the system");
                return;
            }

            _bookManagementService.LoanBook(book);
            _userManagementService.LoanBook(user, book);

            Console.WriteLine("Book successfully loaned");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void ReturnBook()
    {
        Console.WriteLine("Enter ISBN:");
        var isbn = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(isbn))
        {
            Console.WriteLine(NO_ISBN);
            return;
        }

        Console.WriteLine("Enter First Name of user returning book:");
        var firstName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(firstName))
        {
            Console.WriteLine(NO_FIRST_NAME);
            return;
        }

        Console.WriteLine("Enter Last Name of user returning book:");
        var lastName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(lastName))
        {
            Console.WriteLine(NO_LAST_NAME);
            return;
        }

        try
        {
            var book = _bookManagementService.GetBook(isbn);

            if (book == null)
            {
                Console.WriteLine("No book found with that ISBN");
                return;
            }

            var user = _userManagementService.GetUser(firstName, lastName);

            if (user == null)
            {
                Console.WriteLine("No user found with that name in the system");
                return;
            }

            _bookManagementService.ReturnBook(book);
            _userManagementService.ReturnBook(user, book);

            Console.WriteLine("Book successfully returned");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
