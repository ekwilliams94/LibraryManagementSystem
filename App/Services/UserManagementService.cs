using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Services
{
    public interface IUserManagementService
    {
        public void AddUser(string firstName, string lastName, string? email);
        public void RemoveUser(string firstName, string lastName);
        User? GetUser(string firstName, string lastName);
        public void LoanBook(User user, Book book);
        public void ReturnBook(User user, Book book);
    }

    public class UserManagementService : IUserManagementService
    {
        private readonly IUserRepository _userRepository;

        public UserManagementService(IUserRepository userRepository) { 
            _userRepository = userRepository;
        }

        public void AddUser(string firstName, string lastName, string? email)
        {
            var user = new User(firstName, lastName, email);
            _userRepository.Add(user);
        }

        public void RemoveUser(string firstName, string lastName)
        {
            var user = _userRepository.GetByName(firstName, lastName);

            if(user != null)
            {
                _userRepository.Delete(user.Id);
            }
        }

        public User? GetUser(string firstName, string lastName)
        {
            return _userRepository.GetByName(firstName, lastName);
        }

        public void LoanBook(User user, Book book)
        {
            user.Books.Add(book);
            _userRepository.Update(user);
        }

        public void ReturnBook(User user, Book book)
        {
            if(user.Books.FirstOrDefault(b => b.Id == book.Id) != null)
            {
                user.Books.Remove(book);
                _userRepository.Update(user);
            }
        }
    }
}
