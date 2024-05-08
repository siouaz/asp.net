using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OeuilDeSauron.Domain.Models.Project;
using OeuilDeSauron.Models;

namespace OeuilDeSauron.Domain.Queries.ProjectQueries
{
    public class GetAllProjectsQuery : IRequest<List<ProjectResponse>>
    {
    }
}
