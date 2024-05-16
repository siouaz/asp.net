using Microsoft.EntityFrameworkCore;

namespace OeuilDeSauron.Data;

/// <summary>
/// Handles operations over aggregate objects.
/// </summary>
/// <typeparam name="T">Repository aggregate type.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Gets the repository unit of work.
    /// </summary>
    IUnitOfWork UnitOfWork { get; }

    /// <summary>
    /// Gets entity <see cref="DbSet{TEntity}"/>.
    /// </summary>
    DbSet<T> GetDbSet();
}
