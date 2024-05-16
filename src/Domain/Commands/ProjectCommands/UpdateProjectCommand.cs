using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OeuilDeSauron.Models;

namespace OeuilDeSauron.Domain.Commands.ProjectCommands
{
    public class UpdateProjectCommand : IRequest<bool>
    {
        public Project Project { get; set; }
        public string Id { get; set; }
        public UpdateProjectCommand(string id ,Project project)
        {

            Project = project;
            Id = id;

        }
    }
}
