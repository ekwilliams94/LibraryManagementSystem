using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User? GetByName(string firstName, string lastName);
    }

    public class UserRepository : IUserRepository
    {
        private readonly IList<User> _users = new List<User>(); 

        public void Add(User entity)
        {
            if (_users.Any(u => u.FirstName.Equals(entity.FirstName, StringComparison.OrdinalIgnoreCase)
                              && u.LastName.Equals(entity.LastName, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("This user already exists");

            _users.Add(entity);
        }

        public void Delete(Guid id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user != null)
                _users.Remove(user);
        }

        public IEnumerable<User> GetAll() => _users;

        public User? GetById(Guid id) => _users.FirstOrDefault(u => u.Id == id);

        public User? GetByName(string firstName, string lastName)
            => _users.FirstOrDefault(u => u.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase)
                                        && u.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

        public void Update(User entity)
        {
            var user = _users.FirstOrDefault(u => u.Id == entity.Id)
                ?? throw new Exception("User does not exist within repository");

            user.FirstName = entity.FirstName;
            user.LastName = entity.LastName;
            user.Email = entity.Email;
            user.Books = entity.Books;
        }
    }
}
