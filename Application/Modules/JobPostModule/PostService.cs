using AutoMapper;
using Domain.Common.Exceptions;
using Domain.Common.RequestModels.JobPostModule;
using Domain.Entities.PostModule;
using Domain.IRepositories.IGenericRepositories;
using Domain.IServices.IAuthServices;
using Domain.IServices.IEntityServices.IJobPostModule;
using Domain.IServices.IHelperServices;
using System.Net;

namespace Application.Modules.JobPostModule
{
    public class PostService : IPostService
    {
        #region Constructors and Locals

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileUploadService _fileUploadService;
        private readonly ICurrentUserService _currentUserService;
        public PostService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService, IFileUploadService fileUploadService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _fileUploadService = fileUploadService;
        }

        #endregion

        public async Task<bool> ReactToPostAsync(ReactToPostRequest request)
        {
            var reaction = new PostReaction
            {
                fk_PostReactionTypeID = request.ReactionType,
                fk_PostID = request.PostID,
            };
            await _unitOfWork.PostRepository.ReactionRepository.AddAsync(reaction);
            _unitOfWork.Complete();
            return true;
        }
        public async Task<bool> AddCommentAsync(AddCommentRequest request)
        {
            var comment = new PostComment
            {
                Comment = request.Comment,
                fk_PostID = request.PostID,
            };
            await _unitOfWork.PostRepository.CommentRepository.AddAsync(comment);
            _unitOfWork.Complete();
            return true;
        }
        public async Task<bool> CreatePostAsync(CreatePostRequest request)
        {
            if (_currentUserService.IsUserCandidate)
            {
                throw new ClientException("Forbidden!", HttpStatusCode.Forbidden);
            }

            var userId = Convert.ToInt32(_currentUserService.UserID);
            var thisUserCorporateProfile = _unitOfWork.UserRepository.GetUserCorporateProfile(userId);

            var post = new Domain.Entities.PostModule.Post
            {
                Text = request.Text,
                fk_PostTypeID = request.fk_PostTypeID,
                fk_CommentSettingID = request.fk_CommentSettingID,
                fk_PostViewSettingID = request.fk_PostViewSettingID,
                Tags = new List<Domain.Entities.PostModule.PostTag>(request.Tags.Select(x => new Domain.Entities.PostModule.PostTag
                {
                    Text = x
                }))
            };
            await _unitOfWork.PostRepository.AddAsync(post);
            _unitOfWork.Complete();
            return true;
        }
    }
}
