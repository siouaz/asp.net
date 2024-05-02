using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace OeuilDeSauron.Domain.Commands.Project
{
    public class UpdateProjectCommand : IRequest<bool>
    {
        public OeuilDeSauron.Models.Project Project { get; set; }
        public string Id { get; set; }
        public UpdateProjectCommand(string id ,OeuilDeSauron.Models.Project project)
        {

            Project = project;
            Id = id;

        }
    }
}
