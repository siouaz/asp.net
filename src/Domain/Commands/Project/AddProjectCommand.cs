using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace OeuilDeSauron.Domain.Commands.Project
{
    public class AddProjectCommand : IRequest<OeuilDeSauron.Models.Project>
    {
        public OeuilDeSauron.Models.Project Project { get; set; }
        public AddProjectCommand(OeuilDeSauron.Models.Project project)
        {

            Project = project;

        }
    }
}
