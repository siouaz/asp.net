using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using MediatR;
using Models;
using OeuilDeSauron.Data;
using OeuilDeSauron.Domain.Interfaces;
using OeuilDeSauron.Domain.Models;
using OeuilDeSauron.Domain.Queries.CheckHealthQueries;
using OeuilDeSauron.Domain.Queries.ProjectQueries;
using OeuilDeSauron.Models;

namespace OeuilDeSauron.Domain.Handlers.HealthCheckHandlers
{

    public class GetApiHealthHandler : IRequestHandler<GetApiHealthQuery, ApiHealth>
    {
        private readonly MonitoringContext _context;
        private readonly IMyHealthCheck _healthCheck;

        public GetApiHealthHandler(MonitoringContext context, IMyHealthCheck healthCheck)
        {
            _context = context;
            _healthCheck = healthCheck;
        }
        public async Task<ApiHealth> Handle(GetApiHealthQuery request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.FindAsync(request.ProjectId);
            if (project == null)
            {
                throw new NullReferenceException(nameof(request));
            }

            var healthCheckRequest = new HealthCheckRequest
            {
                ProjectId = project.Id,
                ProjectName = project.Name,
                Url = project.HealthcheckUrl,
                Headers = project.Headers
            };

            var result = await _healthCheck.CheckHealthAsync(healthCheckRequest);
            return result;
        }
    }
}
