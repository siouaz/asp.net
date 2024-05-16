using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Models;
using OeuilDeSauron.Domain.Models;
using OeuilDeSauron.Models;

namespace OeuilDeSauron.Domain.Queries.CheckHealthQueries
{
    public class GetNthApiHealthQuery : IRequest<List<ApiHealth>>
    {
        public string ProjectId { get; }
        public int ChecksNumber { get; }

        public GetNthApiHealthQuery(string projectId,int checksNumer)
        {
            ProjectId = projectId;
            ChecksNumber = checksNumer;
        }
    }
}
