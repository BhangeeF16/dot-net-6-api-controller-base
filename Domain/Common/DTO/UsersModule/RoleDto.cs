using AutoMapper;
using Domain.Entities.UsersModule;
using Domain.IServices.IMapperServices;

namespace Domain.Common.DTO.UsersModule
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
