using AutoMapper;
using Domain.Common.MapperService;
using Domain.Entities.CandidateModule;
using Domain.Entities.GeneralModule;

namespace Domain.Common.Models.CandidateModule
{
    public class EducationExperienceDto : IMapFrom<EducationExperienceDto>
    {
        public int ID { get; set; }

        public LevelOfEdujcation LevelOfEdujcation { get; set; }
        public int fk_SubjectID { get; set; }

        public string? InstitueName { get; set; }
        public string? Details { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public bool IsPresentExperience { get; set; }
        public int fk_CandidateProfileID { get; set; }
        public EducationSubjectDto? EducationSubject { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EducationExperienceDto, CandidateEducationExperience>();
            profile.CreateMap<CandidateEducationExperience, EducationExperienceDto>();
        }
    }

    public class EducationSubjectDto : IMapFrom<EducationSubjectDto>
    {
        public int ID { get; set; }
        public string? Subject { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<EducationSubjectDto, EducationSubject>();
            profile.CreateMap<EducationSubject, EducationSubjectDto>();
        }
    }
}
