using API.DTOs.ViewModel;
using API.Model;
using System.Security.Cryptography;
using System.Text;

namespace API.DataAccessLayer
{
    // Repositories/UserRepository.cs
    public class UserRepository : IUserRepository
    {
        private readonly APIDbContext _context;

        public UserRepository(APIDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }


        public UserViewModel ValidateUser(string username, string password)
        {
            // Find user by username
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
                return null; // Username not found

            // Compute SHA256 hash of entered password
            using var sha256 = SHA256.Create();
            var enteredHashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var enteredHashString = BitConverter.ToString(enteredHashBytes).Replace("-", "").ToLower();

            // Compare entered hash with stored hash
           // if (!string.Equals(user.PasswordHash, enteredHashString, StringComparison.OrdinalIgnoreCase))
               // return null; // Password mismatch

            // Return user info if both match
            return new UserViewModel
            {
                Id = user.Id,
                Username = user.Username
            };
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            var existingUser = _context.Users.Find(user.Id);
            if (existingUser != null)
            {
                _context.Entry(existingUser).CurrentValues.SetValues(user);
                _context.SaveChanges();
            }
        }

        public void DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

    }
}
