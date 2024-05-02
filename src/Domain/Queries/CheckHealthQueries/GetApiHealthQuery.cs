using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OeuilDeSauron.Domain.Models;
using OeuilDeSauron.Models;

namespace OeuilDeSauron.Domain.Queries.CheckHealthQueries
{
    public class GetApiHealthQuery : IRequest<HealthCheckResponse>
    {
        public string ProjectId { get; }

        public GetApiHealthQuery(string projectId)
        {
            ProjectId = projectId;
        }
    }
}
