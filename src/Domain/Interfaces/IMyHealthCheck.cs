using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using OeuilDeSauron.Domain.Models;

namespace OeuilDeSauron.Domain.Interfaces
{
    /// <summary>
    /// Represents a health check, which can be used to check the status of a component in the application, such as a backend service, database or some internal
    /// state.
    /// </summary>
    public interface IMyHealthCheck
    {
        /// <summary>
        /// Runs the health check, returning the status of the component being checked.
        /// </summary>
        /// <param name="context">A context object associated with the current execution.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the health check.</param>
        /// <returns>A <see cref="Task{HealthCheckResult}"/> that completes when the health check has finished, yielding the status of the component being checked.</returns>
        Task<HealthCheckResponse> CheckHealthAsync(HealthCheckRequest requestParameters);
    }
}
