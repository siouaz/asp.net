using System.Linq;

using Microsoft.AspNetCore.Identity;

using siwar.Data.Identity;
using Microsoft.EntityFrameworkCore;

namespace siwar.Data.Infrastructure.Repositories
{
    public class UserLoginRepository : IUserLoginRepository
    {
        private readonly siwarContext _context;
        public IUnitOfWork UnitOfWork => _context;
        public DbSet<IdentityUserLogin<string>> GetDbSet() => _context.UserLogins;

        public UserLoginRepository(siwarContext context) =>
            _context = context;

        public IQueryable<IdentityUserLogin<string>> Query() =>
            _context.UserLogins;

    }
}
