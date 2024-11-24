using KartverketProsjekt.Data;
using KartverketProsjekt.Models.DomainModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace KartverketProsjekt.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly KartverketDbContext _context;

        public UserRepository(KartverketDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> DeleteUserById(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
