using AutoMapper;
using Domain.Common.MapperService;
using Domain.Common.Models.CandidateModule;
using Domain.Entities.CandidateModule;
using Domain.Entities.GeneralModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.RequestModels.CandidateModule
{
    public class UpsertEducationExperienceRequest
    {
        public List<UpsertEducationExperienceRequestElement>? EducationExperiences { get; set; }
    }
    public class UpsertEducationExperienceRequestElement : IMapFrom<UpsertEducationExperienceRequestElement>
    {
        public int ID { get; set; }

        public LevelOfEdujcation LevelOfEdujcation { get; set; }
        public int fk_SubjectID { get; set; }

        public string? InstitueName { get; set; }
        public string? Details { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public bool IsPresentExperience { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpsertEducationExperienceRequestElement, CandidateEducationExperience>();
            profile.CreateMap<CandidateEducationExperience, UpsertEducationExperienceRequestElement>();
        }
    }
}
