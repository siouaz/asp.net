using System.Linq;

using Microsoft.AspNetCore.Identity;

namespace siwar.Data.Identity;

public interface IUserLoginRepository : IRepository<IdentityUserLogin<string>>
{
    IQueryable<IdentityUserLogin<string>> Query();
}
