using API.DTOs.ViewModel;
using API.Model;

namespace API.DataAccessLayer
{
    // Repositories/IUserRepository.cs
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        public UserViewModel ValidateUser(string username, string password);
    }

}

