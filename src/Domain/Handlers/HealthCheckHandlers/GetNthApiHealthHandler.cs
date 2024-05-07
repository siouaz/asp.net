using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using MediatR;
using Models;
using OeuilDeSauron.Data;
using OeuilDeSauron.Domain.Commands.ApiHealthCommands;
using OeuilDeSauron.Domain.Interfaces;
using OeuilDeSauron.Domain.Models;
using OeuilDeSauron.Domain.Queries.CheckHealthQueries;
using OeuilDeSauron.Domain.Queries.ProjectQueries;
using OeuilDeSauron.Models;

namespace OeuilDeSauron.Domain.Handlers.HealthCheckHandlers
{

    public class GetNthApiHealthHandler : IRequestHandler<GetNthApiHealthQuery, List<ApiHealth>>
    {
        private readonly MonitoringContext _context;

        public GetNthApiHealthHandler(MonitoringContext context)
        {
            _context = context;
        }
        public async Task<List<ApiHealth>> Handle(GetNthApiHealthQuery request, CancellationToken cancellationToken)
        {
           var result = _context.ApiHealths.OrderByDescending(a=>a.DateTime).Take(request.ChecksNumber).ToList();
            return result;
        }
    }
}
