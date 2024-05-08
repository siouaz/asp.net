using System.Linq;

using AutoMapper;
using OeuilDeSauron.Data.Identity;
using OeuilDeSauron.Domain.Models.Identity;
using OeuilDeSauron.Domain.Models.Project;
using OeuilDeSauron.Models;

namespace OeuilDeSauron.Domain.Mapping;

public class ProjectMapper : Profile
{
    public ProjectMapper()
    {
        CreateMap<Project, ProjectRequest>().ReverseMap();
        CreateMap< ProjectRequest,Project>().ReverseMap();
    }
}
