using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.DataAccess.Entities;
using Identity.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Identity.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityContext _context;

        public UserRepository(IdentityContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserAsync(long id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<long> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Users.Find(user.Id) == null)
                    return false;

                throw;
            }
           
            return true;
        }

        public async Task<bool> DeleteUserAsync(long userId)
        {
            var exist = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (exist == null)
                return false;

            _context.Users.Remove(exist);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}