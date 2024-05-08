using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using MediatR;
using OeuilDeSauron.Data;
using OeuilDeSauron.Domain.Commands.ProjectCommands;
using OeuilDeSauron.Domain.Models.Project;
using OeuilDeSauron.Domain.Queries.ProjectQueries;
using OeuilDeSauron.Models;

namespace OeuilDeSauron.Domain.Handlers.ProjectHandlers
{
    public class AddProjectHandler : IRequestHandler<AddProjectCommand, ProjectResponse>
    {
        private readonly MonitoringContext _context;
        private readonly IMapper _mapper;

        public AddProjectHandler(MonitoringContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ProjectResponse> Handle(AddProjectCommand request, CancellationToken cancellationToken)
        {
            _context.Projects.Add(request.Project);
            await _context.SaveChangesAsync();

            var project = await _context.Projects.FindAsync(request.Project.Id);
            var projectResponse = _mapper.Map<Project, ProjectResponse>(project);
            if (projectResponse == null)
            {
                throw new NullReferenceException();
            }
            return projectResponse;
        }
    }
}
