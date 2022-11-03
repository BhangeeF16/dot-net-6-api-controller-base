using AutoMapper;
using Domain.Common.MapperService;
using Domain.Common.Models.CandidateModule;
using Domain.Entities.CandidateModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.RequestModels.CandidateModule
{
    public class UpsertJobExperienceRequest
    {
        public List<UpsertJobExperienceRequestElement>? JobExperiences { get; set; }
    }

    public class UpsertJobExperienceRequestElement : IMapFrom<UpsertJobExperienceRequestElement>
    {
        public int ID { get; set; }
        public string? CorporateName { get; set; }
        public string? RoleName { get; set; }
        public string? Details { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public bool IsPresentExperience { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpsertJobExperienceRequestElement, CandidateJobExperience>();
            profile.CreateMap<CandidateJobExperience, UpsertJobExperienceRequestElement>();
        }
    }
}
