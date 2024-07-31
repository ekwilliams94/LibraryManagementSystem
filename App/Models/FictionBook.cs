namespace LibraryManagementSystem.Models
{
    public class FictionBook : Book
    {
        public override void DisplayBookInfo()
        {
            Console.WriteLine($"Fiction Book - Title: {Title}, Author: {Author}, Availability: {Availability}");
        }
    }
}
