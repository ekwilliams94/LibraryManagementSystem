namespace LibraryManagementSystem.Models
{
    public class NonfictionBook : Book
    {
        public override void DisplayBookInfo()
        {
            Console.WriteLine($"Nonfiction Book - Title: {Title}, Author: {Author}");
        }
    }
}
