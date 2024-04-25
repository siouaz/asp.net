using System.Threading;
using System.Threading.Tasks;

namespace siwar.Infrastructure.Jobs
{
    /// <summary>
    /// Represents a background job.
    /// </summary>
    public interface IJob<in TData>
    {
        /// <summary>
        /// Gets or sets
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Executes a job.
        /// </summary>
        /// <typeparam name="TData">Job data type.</typeparam>
        /// <param name="data">Job data.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task ExecuteAsync(TData data, CancellationToken cancellationToken = default);
    }
}
