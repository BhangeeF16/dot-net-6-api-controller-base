using Domain.Common.DTO;

namespace Domain.IServices.IEntityServices
{
    public interface IRoleService
    {
        Task<List<RoleDto>> GetAllRoleRequestAsync();
        Task<RoleDto> GetRoleByIdRequestAsync(int Id);
        Task<RoleDto> GetCurrentUserRole();
    }
}
