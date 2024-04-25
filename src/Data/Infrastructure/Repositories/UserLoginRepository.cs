using System.Linq;

using Microsoft.AspNetCore.Identity;

using siwar.Data.Identity;
using Microsoft.EntityFrameworkCore;

namespace siwar.Data.Infrastructure.Repositories
{
    public class UserLoginRepository : IUserLoginRepository
    {
        private readonly MonitoringContext _context;
        public IUnitOfWork UnitOfWork => (IUnitOfWork)_context;
        public DbSet<IdentityUserLogin<string>> GetDbSet() => _context.UserLogins;

        public UserLoginRepository(MonitoringContext context) =>
            _context = context;

        public IQueryable<IdentityUserLogin<string>> Query() =>
            _context.UserLogins;

    }
}
