using Application.Pipeline.Authorization.PolicyAuth;
using Domain.Common.DataAccessHelpers.Pagination;
using Domain.Common.Models.GeneralModels;
using Domain.Common.RequestModels.JobPostModule;
using Domain.IRepositories.IGenericRepositories;
using Domain.IServices.IAuthServices;
using Domain.IServices.IEntityServices.IJobPostModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;

namespace WebAPI.Controllers.JobPostModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : BaseModule
    {
        #region Constructors and Locals

        private readonly IJobService _jobService;
        public JobController(IWebHostEnvironment webHostEnvironment,
                                   ICurrentUserService currentUserService,
                                   IUnitOfWork unitOfWork,
                                   IJobService jobService) : base(unitOfWork, currentUserService, webHostEnvironment)
        {
            _jobService = jobService;
        }

        #endregion

        #region GET
        /// <summary>
        /// Get the public Jobs which anyone can view
        /// </summary>
        /// <param name="pagination">Pagination attributes</param>
        /// <returns>Jobs</returns>
        [HttpGet("/jobs")]
        [AllowAnonymous]
        public async Task<IResult> GetJobsSearch([FromQuery] Pagination pagination)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _jobService.GetJobsSearch(pagination);
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
        /// Create a Job. Only Corporate Users Allowed
        /// </summary>
        /// <param name="request">CreateJobRequest</param>
        /// <returns>Boolean</returns>
        [HttpPost("/jobs")]
        [Authorize(Policy = PolicyLegend.CorporateOnly)]
        public async Task<IResult> CreateJobAsync([FromForm] CreateJobRequest request)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _jobService.CreateJobAsync(request);
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
        /// Apply to Job Candidates Allowed Only
        /// </summary>
        /// <param name="request">ApplyForJobRequest</param>
        /// <returns>Boolean</returns>
        [HttpPost("/jobs/apply")]
        [Authorize(Policy = PolicyLegend.CandidateOnly)]
        public async Task<IResult> ApplyJobAsync([FromForm] ApplyForJobRequest request)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _jobService.ApplyToJobAsync(request);
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
