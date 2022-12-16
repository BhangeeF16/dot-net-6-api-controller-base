using API.Extensions;
using Application.Pipeline.Authorization.PolicyAuth;
using Domain.IRepositories.IGenericRepositories;
using Domain.IServices.IAuthServices;
using Domain.IServices.IEntityServices.IUserModule;
using Domain.Models.AuthModels;
using Domain.Models.GeneralModels;
using Domain.Models.UsersModule;
using Domain.RequestModels.UserRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers.UserModule
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [SwaggerResponse(500, type: typeof(ErrorResponseModel))]
    [SwaggerResponse(400, type: typeof(ErrorResponseModel))]
    [SwaggerResponse(404, type: typeof(ErrorResponseModel))]
    [SwaggerResponse(422, type: typeof(ErrorResponseModel))]
    [SwaggerResponse(304, type: typeof(ErrorResponseModel))]
    public class UserController : BaseModule
    {
        #region Constructors and Locals

        private readonly IUserService _userService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public UserController(IWebHostEnvironment webHostEnvironment,
                              IUserService userService,
                              ICurrentUserService currentUserService,
                              IUnitOfWork unitOfWork,
                              IJwtTokenGenerator jwtTokenGenerator) : base(unitOfWork, currentUserService, webHostEnvironment)
        {
            _userService = userService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        #endregion

        #region GET

        /// <summary>
        /// Gets user by ID
        /// </summary>
        /// <param name="id">ID of user</param>
        /// <returns>user</returns>
        [HttpGet("/users/{id}")]
        [Authorize(Policy = PolicyLegend.CanViewUsers)]
        [SwaggerResponse(200, type: typeof(SuccessResponseModel<UserDto>))]
        public async Task<IResult> GetByIdAsync(int id)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _userService.GetRequestAsync(id);
                return Results.Ok(new SuccessResponseModel<UserDto>()
                {
                    Message = "Success",
                    Result = response,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Success = true
                });
            });
        }

        /// <summary>
        /// Gets Current Logged-In user
        /// </summary>
        /// <returns>user</returns>
        [Authorize]
        [HttpGet("/users/me")]
        [SwaggerResponse(200, type: typeof(SuccessResponseModel<UserDto>))]
        public async Task<IResult> GetMeAsync()
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _userService.GetCurrentUserRequestAsync();
                return Results.Ok(new SuccessResponseModel<UserDto>()
                {
                    Message = "Success",
                    Result = response,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Success = true
                });
            });
        }

        #endregion

        #region POST 

        /// <summary>
        /// user can register themself here
        /// </summary>
        /// <param name="request">user attributes</param>
        /// <returns>Authorized token response to login</returns>
        [AllowAnonymous]
        [HttpPost("/users/register")]
        [SwaggerResponse(200, type: typeof(SuccessResponseModel<UserTokens>))]
        public async Task<IResult> RegisterAsync(RegisterRequestModel request)
        {
            return await CreateResponseAsync(async () =>
            {
                var registerUserReponse = await _userService.RegisterRequestAsync(request);
                var response = await _userService.LoginRequestAsync(registerUserReponse.UserName, registerUserReponse.Password);
                if (response.Success)
                {
                    var tokenResponse = _jwtTokenGenerator.GenerateToken(response.User);
                    return Results.Ok(new SuccessResponseModel<UserTokens>()
                    {
                        Message = response.Message,
                        Result = tokenResponse,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Success = response.Success
                    });
                }
                else
                {
                    return Results.Ok(new SuccessResponseModel<UserTokens>()
                    {
                        Message = response.Message,
                        Result = null,
                        StatusCode = System.Net.HttpStatusCode.Unauthorized,
                        Success = response.Success
                    });
                }
            });
        }
        /// <summary>
        /// user can login here
        /// </summary>
        /// <param name="loginRequest">username, password</param>
        /// <returns>Authorized token response to login</returns>
        [AllowAnonymous]
        [HttpPost("/users/login")]
        [SwaggerResponse(200, type: typeof(SuccessResponseModel<UserTokens>))]
        public async Task<IResult> LoginAsync(LoginRequestModel loginRequest)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _userService.LoginRequestAsync(loginRequest.Email, loginRequest.Password);
                if (response.Success)
                {
                    var tokenResponse = _jwtTokenGenerator.GenerateToken(response.User);
                    return Results.Ok(new SuccessResponseModel<UserTokens>()
                    {
                        Message = response.Message,
                        Result = tokenResponse,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Success = response.Success
                    });
                }
                else
                {
                    return Results.Ok(new SuccessResponseModel<UserTokens>()
                    {
                        Message = response.Message,
                        Result = null,
                        StatusCode = System.Net.HttpStatusCode.Unauthorized,
                        Success = response.Success
                    });
                }
            });
        }

        /// <summary>
        /// Adds a new user
        /// </summary>
        /// <param name="request">user attributes</param>
        /// <returns>user</returns>
        [HttpPost("/users")]
        [Authorize(Policy = PolicyLegend.CanAddUser)]
        [SwaggerResponse(200, type: typeof(SuccessResponseModel<UserDto>))]
        public async Task<IResult> AddAsync(UpsertUserRequest request)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _userService.AddRequestAsync(request);
                return Results.Ok(new SuccessResponseModel<UserDto>()
                {
                    Message = "Success",
                    Result = response,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Success = true
                });
            });
        }

        /// <summary>
        /// user can change password here
        /// </summary>
        /// <param name="request">CurrentPassword, PasswordConfirmation, NewPassword</param>
        /// <returns>Boolean</returns>
        [Authorize]
        [HttpPost("/users/change-password")]
        [SwaggerResponse(200, type: typeof(SuccessResponseModel<bool>))]
        public async Task<IResult> ChangePassword(UpdatePasswordRequestModel request)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _userService.ChangePasswordAsync(request);
                return Results.Ok(new SuccessResponseModel<bool>()
                {
                    Message = response.Message,
                    Result = response.Success,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Success = response.Success
                });
            });
        }

        /// <summary>
        /// used if user forgets his/her password, email will be sent on given username/email if it exists
        /// </summary>
        /// <param name="request">Email</param>
        /// <returns>Boolean</returns>
        [AllowAnonymous]
        [HttpPost("/users/forget-assword")]
        [SwaggerResponse(200, type: typeof(SuccessResponseModel<bool>))]
        public async Task<IResult> ForgetPassword(ForgetPasswordRequestModel request)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _userService.ForgetPasswordRequestAsync(request.Email ?? string.Empty);
                return Results.Ok(new SuccessResponseModel<bool>()
                {
                    Message = response.Message,
                    Result = response.Success,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Success = response.Success
                });
            });
        }

        #endregion

        #region PUT

        /// <summary>
        /// Activates or de-activates a user
        /// </summary>
        /// <param name="id">id of user</param>
        /// <param name="status">true to activate and false for deactive</param>
        /// <returns>boolean</returns>
        [HttpPut("/users/{id}/status/{status}")]
        [Authorize(Policy = PolicyLegend.ApplicationAdminOnly)]
        [SwaggerResponse(200, type: typeof(SuccessResponseModel<bool>))]
        public async Task<IResult> UpdateStatusAsync(int id, bool status)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _userService.StatusUpdateAsync(id, status);
                return Results.Ok(new SuccessResponseModel<bool>()
                {
                    Message = "Success",
                    Result = response,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Success = true
                });
            });
        }

        /// <summary>
        /// Updates a user by ID in given request-body
        /// </summary>
        /// <param name="request">user attributes</param>
        /// <returns>User</returns>
        [HttpPut("/users")]
        [Authorize(Policy = PolicyLegend.CanEditUser)]
        [SwaggerResponse(200, type: typeof(SuccessResponseModel<UserDto>))]
        public async Task<IResult> UpdateAsync(UpsertUserRequest request)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _userService.UpdateRequestAsync(request);
                return Results.Ok(new SuccessResponseModel<UserDto>()
                {
                    Message = "Success",
                    Result = response,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Success = true
                });
            });
        }

        /// <summary>
        ///  updates current logged in User
        /// </summary>
        /// <param name="request">user attributes</param>
        /// <returns>User</returns>
        [Authorize]
        [HttpPut("/users/me")]
        [SwaggerResponse(200, type: typeof(SuccessResponseModel<UserDto>))]
        public async Task<IResult> UpdateMeAsync([FromForm] UpdateCurrentUserRequest request)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _userService.UpdateCurrentUserRequestAsync(request);
                return Results.Ok(new SuccessResponseModel<UserDto>()
                {
                    Message = "Success",
                    Result = response,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Success = true
                });
            });
        }

        /// <summary>
        /// upserts current logged-in users' profile picture
        /// </summary>
        /// <param name="request">prfile picture to update with current</param>
        /// <returns>user</returns>
        [Authorize]
        [HttpPut("/users/me/profile-picture")]
        [SwaggerResponse(200, type: typeof(SuccessResponseModel<UserDto>))]
        public async Task<IResult> UpdateMyProfilePictureAsync([FromForm] UpsertProfilePictureRequest request)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _userService.UpdateProfilePictureRequestAsync(request);
                return Results.Ok(new SuccessResponseModel<UserDto>()
                {
                    Message = "Success",
                    Result = response,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Success = true
                });
            });
        }
        #endregion

        #region Delete 

        /// <summary>
        /// Deletes user by ID
        /// </summary>
        /// <param name="id">ID of user</param>
        /// <returns>Boolean</returns>
        [HttpDelete("/users/{id}")]
        [Authorize(Policy = PolicyLegend.ApplicationAdminOnly)]
        [SwaggerResponse(200, type: typeof(SuccessResponseModel<bool>))]
        public async Task<IResult> DeleteAsync(int id)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _userService.DeleteRequestAsync(id);
                return Results.Ok(new SuccessResponseModel<bool>()
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

