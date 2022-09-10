using Common.Authorization.JWT;
using Common.Authorization.PolicyAuth;
using Domain.Common.DTO;
using Domain.Common.RequestModels;
using Domain.Common.ResponseModels;
using Domain.IRepositories.IGenericRepositories;
using Domain.IServices.IAuthServices;
using Domain.IServices.IEntityServices;
using Domain.IServices.IHelperServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseModule
    {
        #region Constructors and Locals

        private readonly IUserService _userService;
        private IWebHostEnvironment _webHostEnvironment;
        private readonly IUploadImageService _uploadImageService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public UserController(IWebHostEnvironment webHostEnvironment,
                              IUserService userService,
                              ICurrentUserService currentUserService,
                              IUploadImageService uploadImageService,
                              IUnitOfWork unitOfWork,
                              IJwtTokenGenerator jwtTokenGenerator) : base(unitOfWork, currentUserService)
        {
            _webHostEnvironment = webHostEnvironment;
            _userService = userService;
            _uploadImageService = uploadImageService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        #endregion

        #region GET

        [HttpGet("/users/{id}")]
        [Authorize(Policy = PolicyLegend.CanViewUsers)]
        public async Task<IResult> GetByIdAsync(int id)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _userService.GetRequestAsync(id);
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
        [HttpGet("/users/me")]
        public async Task<IResult> GetMeAsync()
        {
            return await CreateResponseAsync(async () =>
            {
                var baseUrl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host;
                var response = await _userService.GetCurrentUserRequestAsync();
                response.Image = string.IsNullOrEmpty(response.Image) ? string.Empty : _uploadImageService.GetImageCompleteUrl(baseUrl, response.Image);
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

        #region POST 

        [AllowAnonymous]
        [HttpPost("/users/register")]
        public async Task<IResult> RegisterAsync(RegisterRequestModel model)
        {
            return await CreateResponseAsync(async () =>
            {
                var registerUserReponse = await _userService.RegisterRequestAsync(model);
                var response = await _userService.LoginRequestAsync(registerUserReponse.UserName, registerUserReponse.Password);
                if (response.Success)
                {
                    var tokenResponse = _jwtTokenGenerator.GenerateToken(response.User);
                    return Results.Ok(new SuccessResponseModel()
                    {
                        Message = response.Message,
                        Result = tokenResponse,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Success = response.Success
                    });
                }
                else
                {
                    return Results.Ok(new SuccessResponseModel()
                    {
                        Message = response.Message,
                        Result = null,
                        StatusCode = System.Net.HttpStatusCode.Unauthorized,
                        Success = response.Success
                    });
                }
            });
        }
        
        [AllowAnonymous]
        [HttpPost("/users/login")]
        public async Task<IResult> LoginAsync(LoginRequestModel loginRequest)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _userService.LoginRequestAsync(loginRequest.Email, loginRequest.Password);
                if (response.Success)
                {
                    var tokenResponse = _jwtTokenGenerator.GenerateToken(response.User);
                    return Results.Ok(new SuccessResponseModel()
                    {
                        Message = response.Message,
                        Result = tokenResponse,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Success = response.Success
                    });
                }
                else
                {
                    return Results.Ok(new SuccessResponseModel()
                    {
                        Message = response.Message,
                        Result = null,
                        StatusCode = System.Net.HttpStatusCode.Unauthorized,
                        Success = response.Success
                    });
                }
            });
        }

        [HttpPost("/users")]
        [Authorize(Policy = PolicyLegend.CanAddUser)]
        public async Task<IResult> AddAsync(UserDto userDto)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _userService.AddRequestAsync(userDto);
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
        [HttpPost("/users/change-password")]
        public async Task<IResult> ChangePassword(ChangePasswordRequestModel request)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _userService.ChangePasswordAsync(request);
                return Results.Ok(new SuccessResponseModel()
                {
                    Message = response.Message,
                    Result = null,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Success = response.Success
                });
            });
        }
        
        [AllowAnonymous]
        [HttpPost("/users/forget-assword")]
        public async Task<IResult> ForgetPassword(ForgetPasswordRequestModel request)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _userService.ForgetPasswordRequestAsync(request.Email ?? string.Empty);
                return Results.Ok(new SuccessResponseModel()
                {
                    Message = response.Message,
                    Result = null,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Success = response.Success
                });
            });
        }

        #endregion

        #region PUT

        [HttpPut("/users")]
        [Authorize(Policy = PolicyLegend.CanEditUser)]
        public async Task<IResult> UpdateAsync(UserDto request)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _userService.UpdateRequestAsync(request);
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
        [HttpPut("/users/me")]
        public async Task<IResult> UpdateMeAsync([FromForm] UserUpdateRequest request)
        {
            return await CreateResponseAsync(async () =>
            {
                var baseUrl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host;
                var physicalPath = _webHostEnvironment.ContentRootPath;
                var thisUsersProfile = request.Image != null ? _uploadImageService.UploadImage(request.Image, baseUrl, physicalPath) : string.Empty;

                var response = await _userService.UpdateCurrentUserRequestAsync(request, thisUsersProfile);
                response.Image = string.IsNullOrEmpty(response.Image) ? string.Empty : _uploadImageService.GetImageCompleteUrl(baseUrl, response.Image);

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

        #region Delete 

        [HttpDelete("/users/{id}")]
        [Authorize(Policy = PolicyLegend.CanDeleteUser)]
        public async Task<IResult> DeleteAsync(int id)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _userService.DeleteRequestAsync(id);
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

