using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OeuilDeSauron.Models;

namespace OeuilDeSauron.Domain.Commands.ProjectCommands
{
    public class AddProjectCommand : IRequest<Project>
    {
        public Project Project { get; set; }
        public AddProjectCommand(Project project)
        {

            Project = project;

        }
    }
}
