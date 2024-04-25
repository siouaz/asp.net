using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using siwar.Data.Identity;
using siwar.Domain.Models.Identity;

namespace siwar.Domain.Queries;

public interface IRoleQueries
{
    /// <summary>
    /// Gets all roles.
    /// </summary>
    Task<IList<RoleModel>> GetAllAsync(CancellationToken cancellationToken = default);


    /// <summary>
    /// Gets eligible role for a role.
    /// </summary>
    Task<IList<RoleModel>> GetAllEligibleRoleModelsAsync(CancellationToken cancellationToken = default);
}
