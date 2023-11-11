using DataLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLibrary.Data.Services
{
    public interface IUserService
    {
        Task<User> GetUser(int userId);
        Task<IEnumerable<User>> GetUsers();
        Task InsertUser(User user);
    }
}