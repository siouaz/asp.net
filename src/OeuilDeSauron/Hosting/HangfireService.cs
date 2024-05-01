using System;
using System.Threading;
using System.Threading.Tasks;

using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using OeuilDeSauron.Infrastructure.Jobs;

namespace OeuilDeSauron.Hosting
{
    /// <summary>
    /// Hangfire recurrent jobs subscription service.
    /// </summary>
    /// <remarks>
    /// This service subscribes hangfire recurrent
    /// jobs when the application starts.
    /// </remarks>
    public class HangfireService : IHostedService
    {
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initialize a new instance of the <see cref="HangfireService"/> class.
        /// </summary>
        /// <param name="recurringJobManager">Hangfire recurrent job manager.</param>
        /// <param name="serviceProvider">Application service provider.</param>
        public HangfireService(IRecurringJobManager recurringJobManager, IServiceProvider serviceProvider)
        {
            _recurringJobManager = recurringJobManager;
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc/>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var recurring = scope.ServiceProvider.GetServices<IRecurringJob>();

            foreach (var job in recurring)
            {
                _recurringJobManager.AddOrUpdate(job.Name, () => job.ExecuteAsync(default), job.Recurrence);
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task StopAsync(CancellationToken cancellationToken) =>
            Task.CompletedTask;
    }
}