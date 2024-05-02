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
    public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, bool>
    {
        private readonly MonitoringContext _context;

        public DeleteProjectHandler(MonitoringContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.FindAsync(request.Id);
            if (project == null)
            {
               throw new NullReferenceException(nameof(project));
            }
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
