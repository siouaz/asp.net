using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OeuilDeSauron.Domain.Models.Project;
using OeuilDeSauron.Models;

namespace OeuilDeSauron.Domain.Commands.ProjectCommands
{
    public class AddProjectCommand : IRequest<ProjectResponse>
    {
        public Project Project { get; set; }
        public AddProjectCommand(Project project)
        {

            Project = project;

        }
    }
}
