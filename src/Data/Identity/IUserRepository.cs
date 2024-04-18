using System;
using System.Threading.Tasks;

namespace siwar.Data.Identity;

/// <summary>
/// <see cref="User"/> repository.
/// </summary>
public interface IUserRepository : IRepository<User>
{
    /// <summary>
    /// Retrieves a user by id with structures.
    /// </summary>
    /// <param name="userId">User id.</param>
    Task<User> GetUserAsync(string userId);
}
