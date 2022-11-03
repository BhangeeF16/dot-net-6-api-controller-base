using AutoMapper;
using Domain.Common.MapperService;
using Domain.Common.Models.CandidateModule;
using Domain.Common.Models.CorporateModule;
using Domain.Entities.UsersModule;

namespace Domain.Common.Models.UserModule
{
    public class UserCandidateProfileDto : IMapFrom<UserCandidateProfileDto>
    {
        public int ID { get; set; }
        public int fk_UserID { get; set; }

        public virtual List<CandidateResumeUploadDetailDto>? CandidateResumeUploadDetails { get; set; }
        public virtual List<JobApplicantDto>? JobApplications { get; set; }
        public virtual List<JobExperienceDto>? JobExperiences { get; set; }
        public virtual List<EducationExperienceDto>? EducationExperiences { get; set; }

        public string? JobTitle { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public string? FacecbookUserName { get; set; }
        public string? LinkedInUserName { get; set; }
        public string? About { get; set; }
        public object model { get; internal set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserCandidateProfileDto, UserCandidateProfile>();
            profile.CreateMap<UserCandidateProfile, UserCandidateProfileDto>();
        }

    }
}
