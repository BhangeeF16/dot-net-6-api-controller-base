using Domain.IRepositories.IGenericRepositories;
using Domain.IServices.IAuthServices;
using Domain.IServices.IEntityServices.IUserModule;
using Domain.Models.GeneralModels;
using Domain.Models.UsersModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Extensions;

namespace WebAPI.Controllers.UserModule
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [SwaggerResponse(500, type: typeof(ErrorResponseModel))]
    [SwaggerResponse(400, type: typeof(ErrorResponseModel))]
    [SwaggerResponse(404, type: typeof(ErrorResponseModel))]
    [SwaggerResponse(422, type: typeof(ErrorResponseModel))]
    [SwaggerResponse(304, type: typeof(ErrorResponseModel))]
    public class RoleController : BaseModule
    {
        #region Constructors and Locals

        private readonly IRoleService _roleService;
        public RoleController(IWebHostEnvironment webHostEnvironment,
                                IRoleService roleService,
                                ICurrentUserService currentUserService,
                                IUnitOfWork unitOfWork
                                ) : base(unitOfWork, currentUserService, webHostEnvironment)
        {
            _roleService = roleService;
        }

        #endregion

        #region GET
        /// <summary>
        /// Get ALL Roles
        /// </summary>
        /// <returns>List of Roles</returns>
        [Authorize]
        [HttpGet("/roles")]
        [SwaggerResponse(200, type: typeof(SuccessResponseModel<List<RoleDto>>))]
        public async Task<IResult> GetAllRoles()
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _roleService.GetAllRoleRequestAsync();
                return Results.Ok(new SuccessResponseModel<List<RoleDto>>()
                {
                    Message = "Success",
                    Result = response,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Success = true
                });
            });
        }

        /// <summary>
        /// Get Role by ID
        /// </summary>
        /// <param name="id">role ID</param>
        /// <returns>Role</returns>
        [Authorize]
        [HttpGet("/roles/{id}")]
        [SwaggerResponse(200, type: typeof(SuccessResponseModel<RoleDto>))]
        public async Task<IResult> GetRoleById(int id)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _roleService.GetRoleByIdRequestAsync(id);
                return Results.Ok(new SuccessResponseModel<RoleDto>()
                {
                    Message = "Success",
                    Result = response,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Success = true
                });
            });
        }

        /// <summary>
        /// Get Current user/Logged-In user Role
        /// </summary>
        /// <returns>Role</returns>
        [Authorize]
        [HttpGet("/users/me/roles")]
        [SwaggerResponse(200, type: typeof(SuccessResponseModel<RoleDto>))]
        public async Task<IResult> GetAllDraftAsync()
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _roleService.GetCurrentUserRole();
                return Results.Ok(new SuccessResponseModel<RoleDto>()
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

