namespace LibraryManagementSystem.Models
{
    public abstract class Book 
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string ISBN { get; set; } = null!;
        public BookAvailability Availability { get; set; }

        public abstract void DisplayBookInfo();
    }

    public enum BookAvailability
    {
        Available,
        Unavailable
    }
}
