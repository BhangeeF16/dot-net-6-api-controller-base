using AutoMapper;
using Domain.Common.Exceptions;
using Domain.Common.Extensions;
using Domain.Common.Models.CandidateModule;
using Domain.Common.Models.UserModule;
using Domain.Common.RequestModels.CandidateModule;
using Domain.Entities.CandidateModule;
using Domain.Entities.UsersModule;
using Domain.IRepositories.IGenericRepositories;
using Domain.IServices.IAuthServices;
using Domain.IServices.IEntityServices.ICandidateModule;
using Domain.IServices.IHelperServices;
using System.Net;

namespace Application.Modules.CandidateModule
{
    public class CandidateService : ICandidateService
    {
        #region Constructors and Locals

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileUploadService _fileUploadService;
        private readonly ICurrentUserService _currentUserService;
        public CandidateService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService, IFileUploadService fileUploadService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _fileUploadService = fileUploadService;
        }

        #endregion

        #region Resume

        public bool UploadCandidateResumeRequest(UploadResumeRequest model)
        {
            if (!_currentUserService.IsUserCandidate)
            {
                throw new ClientException("Forbidden!", HttpStatusCode.Forbidden);
            }

            var userId = Convert.ToInt32(_currentUserService.UserID);
            var thisUserCandidateProfile = _unitOfWork.UserRepository.GetUserCandidateProfile(userId);
            thisUserCandidateProfile ??= new UserCandidateProfile();

            if (model.File != null)
            {
                var key = _fileUploadService.UploadFile(model.File);
                var resumeUpload = new CandidateResumeUploadDetail
                {
                    ResumeFileKey = key,
                    CandidateProfile = thisUserCandidateProfile,
                };

                if (thisUserCandidateProfile.CandidateResumeUploadDetails.Any())
                {
                    thisUserCandidateProfile.CandidateResumeUploadDetails.Add(resumeUpload);
                }
                else
                {
                    thisUserCandidateProfile.CandidateResumeUploadDetails = new List<CandidateResumeUploadDetail>
                    {
                        resumeUpload
                    };
                }
                _unitOfWork.Complete();
                return true;
            }
            else
            {
                throw new ClientException("No Resume Found!", HttpStatusCode.Forbidden);
            }

        }
        public async Task<bool> GenerateMyResumeAsync()
        {
            if (!_currentUserService.IsUserCandidate)
            {
                throw new ClientException("Forbidden!", HttpStatusCode.Forbidden);
            }

            var userId = Convert.ToInt32(_currentUserService.UserID);
            var thisUserCandidateProfile = _unitOfWork.UserRepository.GetUserCandidateProfile(userId);
            return await GenerateResumeOfCandidateAsync(thisUserCandidateProfile.ID);
        }
        public async Task<bool> GenerateResumeOfCandidateAsync(int CandidateProfileID)
        {
            var thisUserCandidateProfile = await _unitOfWork.CandidateRepository.GetFirstOrDefaultAsync(x => x.ID == CandidateProfileID && x.IsActive == true && x.IsDeleted == false, x=> x.User);
            var thisCandidatesEducation = await _unitOfWork.CandidateRepository.EducationExperienceRepository.GetWhereAsync(x => x.fk_CandidateProfileID == thisUserCandidateProfile.ID && x.IsActive == true && x.IsDeleted == false, x => x.EducationSubject);
            var thisCandidatesJobs = await _unitOfWork.CandidateRepository.JobExperienceRepository.GetWhereAsync(x => x.fk_CandidateProfileID == thisUserCandidateProfile.ID && x.IsActive == true && x.IsDeleted == false);

            var resumeHtml = ResumeGeneratorExtension.GenerateResumeHtml(new GenerateResumeModel
            {
                FirstName = thisUserCandidateProfile.User.FirstName,
                LastName = thisUserCandidateProfile.User.LastName,
                Candidate = _mapper.Map<UserCandidateProfileDto>(thisUserCandidateProfile),
                JobExperiences = _mapper.Map<List<JobExperienceDto>>(thisCandidatesJobs),
                EducationExperiences = _mapper.Map<List<EducationExperienceDto>>(thisCandidatesEducation)
            });

            return true;
        }

        #endregion

        #region Candidate Profile

        public async Task<bool> UpsertCandidateInfoAsync(UpsertJobExperienceRequest request)
        {
            if (!_currentUserService.IsUserCandidate)
            {
                throw new ClientException("Forbidden!", HttpStatusCode.Forbidden);
            }

            var userId = Convert.ToInt32(_currentUserService.UserID);
            var thisUserCandidateProfile = _unitOfWork.UserRepository.GetUserCandidateProfile(userId);
            var thisCandidatesJobs = await _unitOfWork.CandidateRepository.JobExperienceRepository.GetWhereAsync(x => x.fk_CandidateProfileID == thisUserCandidateProfile.ID && x.IsActive == true && x.IsDeleted == false);

            if (thisCandidatesJobs.Any())
            {
                thisCandidatesJobs.ToList().ForEach(x =>
                {
                    x.IsActive = false;
                    x.IsDeleted = true;
                });
                var requestJobs = _mapper.Map<List<CandidateJobExperience>>(request.JobExperiences);
                var updateJobIDs = requestJobs.Select(x => x.ID).ToList();
                var theseJobsToUpdate = thisCandidatesJobs.Where(x => updateJobIDs.Contains(x.ID));

                foreach (var job in theseJobsToUpdate)
                {
                    var thisJobInRequest = requestJobs.FirstOrDefault(x => x.ID == job.ID);
                    job.CorporateName = thisJobInRequest.CorporateName;
                    job.RoleName = thisJobInRequest.RoleName;
                    job.Details = thisJobInRequest.Details;
                    job.StartedAt = thisJobInRequest.StartedAt;
                    job.EndedAt = thisJobInRequest.EndedAt;
                    job.IsPresentExperience = thisJobInRequest.IsPresentExperience;
                    job.fk_CandidateProfileID = thisUserCandidateProfile.ID;
                    job.IsActive = true;
                    job.IsDeleted = false;
                }


                var newAddedJobs = requestJobs.Where(x => x.ID == 0).ToList();
                newAddedJobs.ForEach(x =>
                {
                    x.IsActive = true;
                    x.IsDeleted = false;
                    x.fk_CandidateProfileID = thisUserCandidateProfile.ID;
                });
                thisCandidatesJobs.ToList().AddRange(newAddedJobs);

            }
            else
            {
                var thisCandidatesExperiences = _mapper.Map<List<CandidateJobExperience>>(request.JobExperiences);
                thisCandidatesExperiences.ToList().ForEach(x => x.fk_CandidateProfileID = thisUserCandidateProfile.ID);
                await _unitOfWork.CandidateRepository.JobExperienceRepository.AddRangeAsync(thisCandidatesExperiences);
            }

            _unitOfWork.Complete();

            return true;
        }

        public async Task<bool> UpsertCandidateInfoAsync(UpsertEducationExperienceRequest request)
        {
            if (!_currentUserService.IsUserCandidate)
            {
                throw new ClientException("Forbidden!", HttpStatusCode.Forbidden);
            }

            var userId = Convert.ToInt32(_currentUserService.UserID);
            var thisUserCandidateProfile = _unitOfWork.UserRepository.GetUserCandidateProfile(userId);
            var thisCandidatesEducation = await _unitOfWork.CandidateRepository.EducationExperienceRepository.GetWhereAsync(x => x.fk_CandidateProfileID == thisUserCandidateProfile.ID && x.IsActive == true && x.IsDeleted == false, x => x.EducationSubject);

            if (thisCandidatesEducation.Any())
            {
                thisCandidatesEducation.ToList().ForEach(x =>
                {
                    x.IsActive = false;
                    x.IsDeleted = true;
                });
                var requestEducations = _mapper.Map<List<CandidateEducationExperience>>(request.EducationExperiences);
                var updateEducationIDs = requestEducations.Select(x => x.ID).ToList();
                var theseEducationsToUpdate = thisCandidatesEducation.Where(x => updateEducationIDs.Contains(x.ID));

                foreach (var education in theseEducationsToUpdate)
                {
                    var thisJobInRequest = requestEducations.FirstOrDefault(x => x.ID == education.ID);
                    education.InstitueName = thisJobInRequest.InstitueName;
                    education.LevelOfEdujcation = thisJobInRequest.LevelOfEdujcation;
                    education.fk_SubjectID = thisJobInRequest.fk_SubjectID;
                    education.Details = thisJobInRequest.Details;
                    education.StartedAt = thisJobInRequest.StartedAt;
                    education.EndedAt = thisJobInRequest.EndedAt;
                    education.IsPresentExperience = thisJobInRequest.IsPresentExperience;
                    education.fk_CandidateProfileID = thisUserCandidateProfile.ID;
                    education.IsActive = true;
                    education.IsDeleted = false;
                }


                var newAddedEducations = requestEducations.Where(x => x.ID == 0).ToList();
                newAddedEducations.ForEach(x =>
                {
                    x.IsActive = true;
                    x.IsDeleted = false;
                    x.fk_CandidateProfileID = thisUserCandidateProfile.ID;
                });
                thisCandidatesEducation.ToList().AddRange(newAddedEducations);

            }
            else
            {
                var thisCandidatesExperiences = _mapper.Map<List<CandidateEducationExperience>>(request.EducationExperiences);
                thisCandidatesExperiences.ToList().ForEach(x => x.fk_CandidateProfileID = thisUserCandidateProfile.ID);
                await _unitOfWork.CandidateRepository.EducationExperienceRepository.AddRangeAsync(thisCandidatesExperiences);
            }


            _unitOfWork.Complete();

            return true;
        }

        public async Task<bool> UpsertCandidateInfoAsync(UpsertCandidateInfoRequest request)
        {
            if (!_currentUserService.IsUserCandidate)
            {
                throw new ClientException("Forbidden!", HttpStatusCode.Forbidden);
            }

            var userId = Convert.ToInt32(_currentUserService.UserID);
            var thisUserCandidateProfile = _unitOfWork.UserRepository.GetUserCandidateProfile(userId);
            thisUserCandidateProfile.JobTitle = request.JobTitle;
            thisUserCandidateProfile.ContactEmail = request.ContactEmail;
            thisUserCandidateProfile.ContactPhone = request.ContactPhone;
            thisUserCandidateProfile.FacecbookUserName = request.FacecbookUserName;
            thisUserCandidateProfile.LinkedInUserName = request.LinkedInUserName;
            thisUserCandidateProfile.About = request.About;
            _unitOfWork.Complete();

            return await Task.FromResult(true);
        }

        #endregion
    }
}
