using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Models;
using OeuilDeSauron.Data;
using OeuilDeSauron.Domain.Commands.ApiHealthCommands;
using OeuilDeSauron.Domain.Interfaces;
using OeuilDeSauron.Domain.Queries.CheckHealthQueries;

namespace OeuilDeSauron.Domain.Handlers.ApiHealthHandlers
{
    public class AddApiHealthHandler : IRequestHandler<AddApiHealthCommand, ApiHealth>
    {
        private readonly MonitoringContext _context;

        public AddApiHealthHandler(MonitoringContext context)
        {
            _context = context;
        }

        public async Task<ApiHealth> Handle(AddApiHealthCommand request, CancellationToken cancellationToken)
        {
            request.ApiHealth.DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _context.ApiHealths.Add(request.ApiHealth);
            await _context.SaveChangesAsync();

            return request.ApiHealth;
        }
    }
}
