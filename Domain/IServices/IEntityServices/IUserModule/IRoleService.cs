﻿using Domain.Common.Models.UserModule;

namespace Domain.IServices.IEntityServices.IUserModule
{
    public interface IRoleService
    {
        Task<List<RoleDto>> GetAllRoleRequestAsync();
        Task<RoleDto> GetRoleByIdRequestAsync(int Id);
        Task<RoleDto> GetCurrentUserRole();
    }
}
