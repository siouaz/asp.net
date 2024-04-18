using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using siwar.Data.Identity;

namespace siwar.Data.Infrastructure.Repositories
{
    /// <inheritdoc/>
    public class UserRepository : IUserRepository
    {
        private readonly siwarContext _context;

        /// <inheritdoc/>
        public IUnitOfWork UnitOfWork => _context;
        public DbSet<User> GetDbSet() => _context.Users;

        public UserRepository(siwarContext context) =>
            _context = context;

        /// <inheritdoc/>
        public async Task<User> GetUserAsync(string userId) =>
            await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

    }
}
