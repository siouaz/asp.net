using System.Linq;

using Microsoft.AspNetCore.Identity;

namespace OeuilDeSauron.Data.Identity;

public interface IUserLoginRepository : IRepository<IdentityUserLogin<string>>
{
    IQueryable<IdentityUserLogin<string>> Query();
}
