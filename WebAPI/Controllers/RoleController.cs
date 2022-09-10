using Domain.Common.ResponseModels;
using Domain.IRepositories.IGenericRepositories;
using Domain.IServices.IAuthServices;
using Domain.IServices.IEntityServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseModule
    {
        #region Constructors and Locals

        private readonly IRoleService _roleService;
        public RoleController(
                                IRoleService roleService,
                                ICurrentUserService currentUserService,
                                IUnitOfWork unitOfWork
                                ) : base(unitOfWork, currentUserService)
        {
            _roleService = roleService;
        }

        #endregion

        #region GET

        [Authorize]
        [HttpGet("/roles")]
        public async Task<IResult> GetAllRoles()
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _roleService.GetAllRoleRequestAsync();
                return Results.Ok(new SuccessResponseModel()
                {
                    Message = "Success",
                    Result = response,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Success = true
                });
            });
        }

        [Authorize]
        [HttpGet("/roles/{id}")]
        public async Task<IResult> GetRoleById(int id)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _roleService.GetRoleByIdRequestAsync(id);
                return Results.Ok(new SuccessResponseModel()
                {
                    Message = "Success",
                    Result = response,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Success = true
                });
            });
        }

        [Authorize]
        [HttpGet("/users/me/roles")]
        public async Task<IResult> GetAllDraftAsync()
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _roleService.GetCurrentUserRole();
                return Results.Ok(new SuccessResponseModel()
                {
                    Message = "Success",
                    Result = response,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Success = true
                });
            });
        }
        #endregion
    }
}

