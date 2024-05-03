using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OeuilDeSauron.Models;

namespace OeuilDeSauron.Domain.Queries.ProjectQueries
{
    public class GetAllProjectsQuery : IRequest<List<Project>>
    {
    }
}
