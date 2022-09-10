using Application.EntityServices;
using Common.Authorization.PolicyAuth;
using Domain.Common.RequestModels;
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
    public class AppSettingController : BaseModule
    {
        #region Constructors and Locals

        private readonly IAppSettingService _appsetting;
        public AppSettingController(
                                AppsSettingService appsetting,
                                ICurrentUserService currentUserService,
                                IUnitOfWork unitOfWork
                                ) : base(unitOfWork, currentUserService)
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
