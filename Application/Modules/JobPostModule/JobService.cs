using AutoMapper;
using Domain.Common.DataAccessHelpers.Pagination;
using Domain.Common.Exceptions;
using Domain.Common.Models.JobPostModule;
using Domain.Common.RequestModels.JobPostModule;
using Domain.Entities.UsersModule;
using Domain.IRepositories.IGenericRepositories;
using Domain.IServices.IAuthServices;
using Domain.IServices.IEntityServices.IJobPostModule;
using Domain.IServices.IHelperServices;
using System.Net;

namespace Application.Modules.JobPostModule
{
    public class JobService : IJobService
    {
        #region Constructors and Locals

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileUploadService _fileUploadService;
        private readonly ICurrentUserService _currentUserService;
        public JobService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService, IFileUploadService fileUploadService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _fileUploadService = fileUploadService;
        }

        #endregion

        public async Task<PaginatedList<JobForJobsPage>> GetJobsSearch(Pagination pagination)
        {
            return await _unitOfWork.JobRepository.GetJobsSearch<JobForJobsPage>(pagination);
        }

        public async Task<bool> CreateJobAsync(CreateJobRequest request)
        {
            if (_currentUserService.IsUserCandidate)
            {
                throw new ClientException("Forbidden!", HttpStatusCode.Forbidden);
            }

            var userId = Convert.ToInt32(_currentUserService.UserID);
            var thisUserCorporateProfile = _unitOfWork.UserRepository.GetUserCorporateProfile(userId);

            var jobPost = new Domain.Entities.CorporateModule.CorporateJob
            {
                JobTitle = request.JobTitle,
                JobType = request.JobType,
                WorkPlaceType = request.WorkPlaceType,
                Location = request.Location,
                fk_CorporateID = thisUserCorporateProfile.fk_CorporateID,
                fk_JobPostedByProfileID = thisUserCorporateProfile.ID,
                Post = new Domain.Entities.PostModule.Post
                {
                    Text = request.Description,
                    fk_PostTypeID = JobPostConstants.PostTypeJob,
                    fk_CommentSettingID = JobPostConstants.PostCommenntAll,
                    fk_PostViewSettingID = JobPostConstants.PostViewAll,
                    Tags = new List<Domain.Entities.PostModule.PostTag>(request.Tags.Select(x => new Domain.Entities.PostModule.PostTag
                    {
                        Text = x
                    })),
                }
            };

            if (request.Files != null  &&  request.Files.Count > 0)
            {
                jobPost.Post.Files = new List<Domain.Entities.PostModule.PostFile>(request.Files.Select(x => new Domain.Entities.PostModule.PostFile
                {
                    FileKey = _fileUploadService.UploadFile(x, JobPostConstants.JobPostDirectory),
                    MimeType = _fileUploadService.GetMimeType(x.FileName)
                }));
            }

            await _unitOfWork.JobRepository.AddAsync(jobPost);
            _unitOfWork.Complete();
            return true;
        }
        public async Task<bool> ApplyToJobAsync(ApplyForJobRequest model)
        {
            if (!_currentUserService.IsUserCandidate)
            {
                throw new ClientException("Forbidden!", HttpStatusCode.Forbidden);
            }

            var userId = Convert.ToInt32(_currentUserService.UserID);
            var thisUserCandidateProfile = _unitOfWork.UserRepository.GetUserCandidateProfile(userId);
            thisUserCandidateProfile ??= new UserCandidateProfile();

            var jobApplication = new Domain.Entities.CorporateModule.JobApplicant
            {
                CandidateProfile = thisUserCandidateProfile,
                fk_CorporateJobID = model.JobID,
            };

            if (model.File != null && model.ResumeID == 0)
            {
                var key = _fileUploadService.UploadFile(model.File);
                jobApplication.CandidateResume = new Domain.Entities.CandidateModule.CandidateResumeUploadDetail
                {
                    ResumeFileKey = key,
                    CandidateProfile = thisUserCandidateProfile,
                };
            }
            else
            {
                jobApplication.fk_CandidateResumeID = (int)model.ResumeID;
            }

            await _unitOfWork.JobRepository.JobApplicantRepository.AddAsync(jobApplication);
            _unitOfWork.Complete();
            return true;
        }
    }
}
