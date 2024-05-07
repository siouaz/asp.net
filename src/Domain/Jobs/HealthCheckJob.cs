using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OeuilDeSauron.Data;
using OeuilDeSauron.Domain.Queries.CheckHealthQueries;

namespace OeuilDeSauron.Domain.Jobs
{
    public class HealthCheckJob
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly MonitoringContext _dbContext;
        private readonly IMediator _mediator;

        public HealthCheckJob(IHttpClientFactory httpClientFactory, MonitoringContext dbContext)
        {
            _httpClientFactory = httpClientFactory;
            _dbContext = dbContext;
        }

        public async Task RunAsync(string projectId)
        {
            var project = await _dbContext.Projects.FindAsync(projectId);

            if (project == null)
                return;

            if (!project.IsActive)
                return;

            var query = new GetApiHealthQuery(projectId);
            var result = await _mediator.Send(query);
        }
    }
}
