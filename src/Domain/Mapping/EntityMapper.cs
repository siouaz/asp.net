using System.Linq;

using AutoMapper;
using siwar.Data.Identity;
using siwar.Domain.Models.Identity;

namespace siwar.Domain.Mapping;

public class EntityMapper : Profile
{
    public EntityMapper()
    {
        CreateMap<Role, RoleModel>();

        CreateMap<User, ProfileModel>()
            .ForMember(x => x.Active, opt => opt.MapFrom(x => !x.IsLockedOut));

        CreateMap<User, UserModel>()
            .ForMember(x => x.Roles, opt => opt.MapFrom(u => u.Roles.Select(r => r.RoleId)))
            .ForMember(x => x.Active, opt => opt.MapFrom(u => !u.IsLockedOut));
    }
}
