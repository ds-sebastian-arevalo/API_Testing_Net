using API_Usuarios.Models;

namespace API_Usuarios.Services
{
    public interface IUserService
    {
        public IEnumerable<User> GetUserList();
        public User AddUser(User user);
        public User UpdateUser(User user);
        public bool DeleteUser(int userId);
    }
}
