using Application.Modules.JobPostModule;
using Application.Pipeline.Authorization.PolicyAuth;
using Domain.Common.Models.GeneralModels;
using Domain.Common.RequestModels.JobPostModule;
using Domain.IRepositories.IGenericRepositories;
using Domain.IServices.IAuthServices;
using Domain.IServices.IEntityServices.ICandidateModule;
using Domain.IServices.IEntityServices.IJobPostModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;

namespace WebAPI.Controllers.JobPostModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : BaseModule
    {
        #region Constructors and Locals

        private readonly IPostService _postService;
        public PostController(IWebHostEnvironment webHostEnvironment,
                                   ICurrentUserService currentUserService,
                                   IUnitOfWork unitOfWork,
                                   IPostService postService) : base(unitOfWork, currentUserService, webHostEnvironment)
        {
            _postService = postService;
        }

        #endregion

        #region POST

        /// <summary>
        /// Create a Post All Users Allowed
        /// </summary>
        /// <param name="request">CreatePostRequest</param>
        /// <returns>Boolean</returns>
        [HttpPost("/posts")]
        [Authorize]
        public async Task<IResult> CreatePostAsync([FromForm] CreatePostRequest request)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _postService.CreatePostAsync(request);
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
        /// Adds a comment to given post
        /// </summary>
        /// <param name="request">AddCommentRequest</param>
        /// <returns>Boolean</returns>
        [HttpPost("/posts/comments")]
        [Authorize]
        public async Task<IResult> AddCommentsAsync(AddCommentRequest request)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _postService.AddCommentAsync(request);
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
        ///  Users Reaction to post submitted
        /// </summary>
        /// <param name="request">ReactToPostRequest</param>
        /// <returns>Boolean</returns>
        [HttpPost("/posts/reactions")]
        [Authorize]
        public async Task<IResult> ReactToPostAsync(ReactToPostRequest request)
        {
            return await CreateResponseAsync(async () =>
            {
                var response = await _postService.ReactToPostAsync(request);
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
