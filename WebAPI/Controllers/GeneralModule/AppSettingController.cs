using Application.Modules.GeneralModule;
using Application.Pipeline.Authorization.PolicyAuth;
using Domain.Common.Models.GeneralModels;
using Domain.Common.RequestModels.GeneralRequests;
using Domain.IRepositories.IGenericRepositories;
using Domain.IServices.IAuthServices;
using Domain.IServices.IEntityServices.IGenralModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;

namespace WebAPI.Controllers.GeneralModule
{
    [Route("api/[controller]")]
    [ApiController]
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
        [Authorize(Policy = PolicyLegend.AdminOnly)]
        public async Task<IResult> GetAllAsync()
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _appsetting.ListRequestAsync();
                return Results.Ok(new SuccessResponseModel()
                {
                    Message = "Success",
                    Result = response,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Success = true
                });
            });
        }
        [HttpGet("/settings/smtp")]
        [Authorize(Policy = PolicyLegend.AdminOnly)]
        public async Task<IResult> GetSmtpCredentialsAsync()
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _appsetting.GetSmtpCredentials();
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

        #region PUT

        [HttpPut("/settings/smtp")]
        [Authorize(Policy = PolicyLegend.AdminOnly)]
        public async Task<IResult> UpsertAsync(SmtpCredentialModel model)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _appsetting.UpsertRequestAsync(model);
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
