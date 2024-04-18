using System.Threading;
using System.Threading.Tasks;

using siwar.Data.Identity;
using siwar.Data.Pagination;
using siwar.Domain.Models.Identity;

namespace siwar.Domain.Queries;

public interface IUserQueries
{
    /// <summary>
    /// Gets all users.
    /// </summary>
    Task<PagedList<UserModel>> GetAllAsync(FilterPaginationOptions options,
        CancellationToken cancellation = default);

    Task<UserModel> GetByIdForAdminInterfaceAsync(string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets user, return null if user does not exists or is not eligible to authenticated user.
    /// Includes certifications.
    /// Includes structures.
    /// </summary>
    Task<User> GetByIdForAdminInterfaceEntityAsync(string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets user by id.
    /// </summary>
    Task<User> GetByIdEntityAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns current user profile.
    /// </summary>
    /// <param name="cancellationToken"></param>
    Task<ProfileModel> GetProfileAsync(CancellationToken cancellationToken = default);
}
