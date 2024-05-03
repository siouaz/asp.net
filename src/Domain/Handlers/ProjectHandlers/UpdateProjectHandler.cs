using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OeuilDeSauron.Data;
using OeuilDeSauron.Domain.Commands.ProjectCommands;
using OeuilDeSauron.Domain.Queries.ProjectQueries;
using OeuilDeSauron.Models;

namespace OeuilDeSauron.Domain.Handlers.ProjectHandlers
{
    public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, bool>
    {
        private readonly MonitoringContext _context;

        public UpdateProjectHandler(MonitoringContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {

            _context.Entry(request.Project).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
