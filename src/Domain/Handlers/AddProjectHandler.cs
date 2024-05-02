using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OeuilDeSauron.Data;
using OeuilDeSauron.Domain.Commands.Project;
using OeuilDeSauron.Domain.Queries.Projects;
using OeuilDeSauron.Models;

namespace OeuilDeSauron.Domain.Handlers
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