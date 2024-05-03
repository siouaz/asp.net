using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace OeuilDeSauron.Domain.Commands.ProjectCommands
{
    public class DeleteProjectCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public DeleteProjectCommand(string id)
        {
            Id = id;

        }
    }
}
