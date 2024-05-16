using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OeuilDeSauron.Data;
using OeuilDeSauron.Domain.Models.Project;
using OeuilDeSauron.Models;

namespace OeuilDeSauron.Domain.Queries.ProjectQueries
{
    public class GetProjectQuery : IRequest<ProjectResponse>
    {
        public string ProjectId { get; }

        public GetProjectQuery(string projectId)
        {
            ProjectId = projectId;
        }
    }
}
