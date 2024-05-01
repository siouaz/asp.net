using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OeuilDeSauron.Data.Applications;

/// <summary>
/// <see cref="IApplicationRepository"/> repository.
/// </summary>
public interface IApplicationRepository : IRepository<Application>
{
    Task<Application> GetApplication(string apiKey, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all registered applications.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task<IList<Application>> GetAllAsync(CancellationToken cancellationToken = default);
}
