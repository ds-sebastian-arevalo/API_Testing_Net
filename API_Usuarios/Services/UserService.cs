//using Microsoft.EntityFrameworkCore;

using API_Usuarios.Models;
using API_Usuarios.Responses;
using Microsoft.EntityFrameworkCore;

namespace API_Usuarios.Services
{
    public class UserService: IUserService
    {
        private readonly API_Context _dbContext;

        public UserService(API_Context dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<User> GetUserList()
        { 
            return _dbContext.Users.ToList();
        }

        public User AddUser(User user)
        {
            var result = _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return result.Entity;
        }

        public User UpdateUser(User user)
        {
            var result = _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
            return result.Entity;
        }

        public bool DeleteUser(int userId)
        {
            var userToDelete = _dbContext.Users.Where(x => x.Id == userId).FirstOrDefault();

            try
            {
                var result = _dbContext.Remove(userToDelete);
                _dbContext.SaveChanges();
                return true;
            } catch
            {
                throw new UserNotExistException();
            }

        }
    }
}
