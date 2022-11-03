using AutoMapper;
using Domain.Common.MapperService;
using Domain.Common.Models.CorporateModule;
using Domain.Entities.CandidateModule;

namespace Domain.Common.Models.CandidateModule
{
    public class CandidateResumeUploadDetailDto : IMapFrom<CandidateResumeUploadDetailDto>
    {
        public int ID { get; set; }
        public string? ResumeFileKey { get; set; }
        public int fk_CandidateProfileID { get; set; }

        public List<JobApplicantDto>? Applications { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CandidateResumeUploadDetailDto, CandidateResumeUploadDetail>();
            profile.CreateMap<CandidateResumeUploadDetail, CandidateResumeUploadDetailDto>();
        }
    }
}
