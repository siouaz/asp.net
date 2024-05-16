using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OeuilDeSauron.Data;
using OeuilDeSauron.Domain.Models.Project;
using OeuilDeSauron.Domain.Queries.ProjectQueries;
using OeuilDeSauron.Models;

namespace OeuilDeSauron.Domain.Handlers.ProjectHandlers
{
    public class GetProjectHandler : IRequestHandler<GetProjectQuery, ProjectResponse>
    {
        private readonly MonitoringContext _context;
        private readonly IMapper _mapper;

        public GetProjectHandler(MonitoringContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ProjectResponse> Handle(GetProjectQuery request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.FindAsync(request.ProjectId);
            var projectResponse = _mapper.Map<Project,ProjectResponse>(project);
            return projectResponse;
        }
    }
}
