using System.Threading;
using System.Threading.Tasks;

namespace OeuilDeSauron.Infrastructure.Jobs
{
    /// <summary>
    /// Represents a background recurring job.
    /// </summary>
    public interface IRecurringJob
    {
        /// <summary>
        /// Gets or sets job name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets or sets job recurrence.
        /// </summary>
        /// <seealso href="https://en.wikipedia.org/wiki/Cron"/>
        public string Recurrence { get; }

        /// <summary>
        /// Executes a job.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task ExecuteAsync(CancellationToken cancellationToken = default);
    }
}