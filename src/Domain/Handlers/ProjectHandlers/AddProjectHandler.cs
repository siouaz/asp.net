using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using MediatR;
using OeuilDeSauron.Data;
using OeuilDeSauron.Domain.Commands.ProjectCommands;
using OeuilDeSauron.Domain.Queries.ProjectQueries;
using OeuilDeSauron.Models;

namespace OeuilDeSauron.Domain.Handlers.ProjectHandlers
{
    public class AddProjectHandler : IRequestHandler<AddProjectCommand, Project>
    {
        private readonly MonitoringContext _context;

        public AddProjectHandler(MonitoringContext context)
        {
            _context = context;
        }
        public async Task<Project> Handle(AddProjectCommand request, CancellationToken cancellationToken)
        {
            _context.Projects.Add(request.Project);
            await _context.SaveChangesAsync();

            var project = await _context.Projects.FindAsync(request.Project.Id);
            if (project == null)
            {
                throw new NullReferenceException();
            }
            return project;
        }
    }
}
