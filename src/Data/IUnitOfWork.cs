using System.Threading;
using System.Threading.Tasks;

namespace OeuilDeSauron.Data;

/// <summary>
/// Encapsulates a unit of work.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Saves changes to all objects that have changed within the unit of work.
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
