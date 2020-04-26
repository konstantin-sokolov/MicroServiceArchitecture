using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.DataAccess.Entities;

namespace Identity.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserAsync(long id);
        Task<long> CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(long userId);
    }
}