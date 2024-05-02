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
    public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsQuery, List<Project>>
    {
        private readonly MonitoringContext _context;

        public GetAllProjectsHandler(MonitoringContext context)
        {
            _context = context;
        }
        public async Task<List<Project>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = _context.Projects.ToList();
            return projects;
        }
    }
}
