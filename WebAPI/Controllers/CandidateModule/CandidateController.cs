using Application.Pipeline.Authorization.PolicyAuth;
using Domain.Common.Models.GeneralModels;
using Domain.Common.RequestModels.CandidateModule;
using Domain.IRepositories.IGenericRepositories;
using Domain.IServices.IAuthServices;
using Domain.IServices.IEntityServices.ICandidateModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;

namespace WebAPI.Controllers.CandidateModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : BaseModule
    {
        #region Constructors and Locals

        private readonly ICandidateService _candidateService;
        public CandidateController(IWebHostEnvironment webHostEnvironment,
                                   ICurrentUserService currentUserService,
                                   IUnitOfWork unitOfWork,
                                   ICandidateService candidateService) : base(unitOfWork, currentUserService, webHostEnvironment)
        {
            _candidateService = candidateService;
        }

        #endregion

        #region GET

        /// <summary>
        /// Get Current/Logged-In User Resume/CV
        /// </summary>
        /// <returns>General Response Model</returns>
        [HttpGet("/candidates/me/generate-resume")]
        [Authorize(Policy = PolicyLegend.CandidateOnly)]
        public async Task<IResult> GenerateMyResumeAsync()
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _candidateService.GenerateMyResumeAsync();
                return Results.Ok(new SuccessResponseModel()
                {
                    Message = "Success",
                    Result = response,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Success = true
                });
            });
        }

        /// <summary>
        /// Get Resume/CV of user by their Candidate profile ID
        /// </summary>
        /// <returns>General Response Model</returns>
        [HttpGet("/candidates/{id}/generate-resume")]
        [Authorize]
        public async Task<IResult> GenerateCandidateResumeAsync(int id)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _candidateService.GenerateResumeOfCandidateAsync(id);
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

        /// <summary>
        /// Upserts Current/Logged-In User Contact/Info
        /// </summary>
        /// <param name="request">UpsertCandidateInfoRequest</param>
        /// <returns>General Response Model</returns>
        [HttpPut("/candidates/me/info")]
        [Authorize(Policy = PolicyLegend.CandidateOnly)]
        public async Task<IResult> UpsertCandidateInfoAsync(UpsertCandidateInfoRequest request)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _candidateService.UpsertCandidateInfoAsync(request);
                return Results.Ok(new SuccessResponseModel()
                {
                    Message = "Success",
                    Result = response,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Success = true
                });
            });
        }

        /// <summary>
        /// Upserts Current/Logged-In User Job Experiences
        /// </summary>
        /// <param name="request">UpsertJobExperienceRequest</param>
        /// <returns>General Response Model</returns>
        [HttpPut("/candidates/me/experiences/jobs")]
        [Authorize(Policy = PolicyLegend.CandidateOnly)]
        public async Task<IResult> UpsertCandidateInfoAsync(UpsertJobExperienceRequest request)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _candidateService.UpsertCandidateInfoAsync(request);
                return Results.Ok(new SuccessResponseModel()
                {
                    Message = "Success",
                    Result = response,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Success = true
                });
            });
        }

        /// <summary>
        /// Upserts Current/Logged-In User Education Experiences
        /// </summary>
        /// <param name="request">UpsertEducationExperienceRequest</param>
        /// <returns>General Response Model</returns>
        [HttpPut("/candidates/me/experiences/educations")]
        [Authorize(Policy = PolicyLegend.CandidateOnly)]
        public async Task<IResult> UpsertCandidateInfoAsync(UpsertEducationExperienceRequest request)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _candidateService.UpsertCandidateInfoAsync(request);
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

        /// <summary>
        /// Use this to Upload Resume/CV of a Candidate or job Applicant Entity
        /// </summary>
        /// <param name="request">UploadResumeRequest</param>
        /// <returns>General Response Model</returns>
        [HttpPost("/candidates/me/resume")]
        [Authorize(Policy = PolicyLegend.CandidateOnly)]
        public async Task<IResult> UploadResumeAsync([FromForm] UploadResumeRequest request)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await Task.FromResult(_candidateService.UploadCandidateResumeRequest(request));
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
