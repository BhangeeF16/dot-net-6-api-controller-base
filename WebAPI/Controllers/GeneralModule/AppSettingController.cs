using Application.Pipeline.Authorization.PolicyAuth;
using Domain.IRepositories.IGenericRepositories;
using Domain.IServices.IAuthServices;
using Domain.IServices.IEntityServices.IGeneralModule;
using Domain.Models.GeneralModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebAPI.Extensions;

namespace WebAPI.Controllers.GeneralModule
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [SwaggerResponse(500, type: typeof(ErrorResponseModel))]
    [SwaggerResponse(400, type: typeof(ErrorResponseModel))]
    [SwaggerResponse(404, type: typeof(ErrorResponseModel))]
    [SwaggerResponse(422, type: typeof(ErrorResponseModel))]
    [SwaggerResponse(304, type: typeof(ErrorResponseModel))]
    public class AppSettingController : BaseModule
    {
        #region Constructors and Locals

        private readonly IAppSettingService _appsetting;
        public AppSettingController(IWebHostEnvironment webHostEnvironment,
                                IAppSettingService appsetting,
                                ICurrentUserService currentUserService,
                                IUnitOfWork unitOfWork
                                ) : base(unitOfWork, currentUserService, webHostEnvironment)
        {
            _appsetting = appsetting;
        }

        #endregion

        #region Get

        [HttpGet("/application/settings")]
        [Authorize(Policy = PolicyLegend.ApplicationAdminOnly)]
        [SwaggerResponse(200, type: typeof(SuccessResponseModel<List<AppSettingModel>>))]
        public async Task<IResult> GetAllAsync()
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _appsetting.ListRequestAsync();
                return Results.Ok(new SuccessResponseModel<List<AppSettingModel>>()
                {
                    Message = "Success",
                    Result = response,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Success = true
                });
            });
        }
        [HttpGet("/settings/smtp")]
        [Authorize(Policy = PolicyLegend.ApplicationAdminOnly)]
        [SwaggerResponse(200, type: typeof(SuccessResponseModel<SmtpCredentialModel>))]
        public async Task<IResult> GetSmtpCredentialsAsync()
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _appsetting.GetSmtpCredentials();
                return Results.Ok(new SuccessResponseModel<SmtpCredentialModel>()
                {
                    Message = "Success",
                    Result = response,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Success = true
                });
            });
        }

        #endregion

        #region PUT

        [HttpPut("/settings/smtp")]
        [Authorize(Policy = PolicyLegend.ApplicationAdminOnly)]
        [SwaggerResponse(200, type: typeof(SuccessResponseModel<SmtpCredentialModel>))]
        public async Task<IResult> UpsertAsync(SmtpCredentialModel model)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _appsetting.UpsertRequestAsync(model);
                return Results.Ok(new SuccessResponseModel<SmtpCredentialModel>()
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
