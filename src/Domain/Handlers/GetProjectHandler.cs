using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OeuilDeSauron.Data;
using OeuilDeSauron.Domain.Queries.Projects;
using OeuilDeSauron.Models;

namespace OeuilDeSauron.Domain.Handlers
{
    public class GetProjectHandler : IRequestHandler<GetProjectQuery, Project>
    {
        private readonly MonitoringContext _context;

        public GetProjectHandler( MonitoringContext context)
        {
            _context = context;
        }
        public async Task<Project> Handle(GetProjectQuery request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.FindAsync(request.ProjectId);
            return project;
        }
    }
}
