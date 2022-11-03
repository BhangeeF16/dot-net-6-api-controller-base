using AutoMapper;
using Domain.Common.MapperService;
using Domain.Entities.CandidateModule;

namespace Domain.Common.Models.CandidateModule
{
    public class JobExperienceDto : IMapFrom<JobExperienceDto>
    {
        public int ID { get; set; }
        public string? CorporateName { get; set; }
        public string? RoleName { get; set; }
        public string? Details { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public bool IsPresentExperience { get; set; }
        public int fk_CandidateProfileID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<JobExperienceDto, CandidateJobExperience>();
            profile.CreateMap<CandidateJobExperience, JobExperienceDto>();
        }
    }
}
