namespace LibraryManagementSystem.Models
{
    public class User(string firstName, string lastName, string? email)
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public string FullName => $"{FirstName} {LastName}";
        public string? Email { get; set; } = email;
        public List<Book> Books { get; set; } = [];

        public void DisplayUserInfo()
        {
            Console.WriteLine($"Name: {FullName}, Email: {Email}");
        }
    }
}
