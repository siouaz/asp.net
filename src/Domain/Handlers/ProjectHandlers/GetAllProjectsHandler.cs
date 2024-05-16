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
    public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsQuery, List<ProjectResponse>>
    {
        private readonly MonitoringContext _context;
        private readonly IMapper _mapper;

        public GetAllProjectsHandler(MonitoringContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ProjectResponse>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = _context.Projects.ToList();
            var projectsResponse = _mapper.Map<List<Project>,List<ProjectResponse>>(projects);
            return projectsResponse;
        }
    }
}
