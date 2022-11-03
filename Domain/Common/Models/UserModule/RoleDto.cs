using AutoMapper;
using Domain.Common.MapperService;
using Domain.Entities.UsersModule;

namespace Domain.Common.Models.UserModule
{
    public class RoleDto : PermissionsToRole, IMapFrom<RoleDto>
    {
        public int Id { get; set; }
        public string? RoleName { get; set; }

        public bool? IsActive { get; set; } = true;
        public bool? IsDeleted { get; set; } = false;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<RoleDto, UserRole>();
            profile.CreateMap<UserRole, RoleDto>();
        }
    }
}
