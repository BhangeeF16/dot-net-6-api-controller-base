using Domain.Common.Models.UserModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace Domain.Common.Models.CandidateModule
{
    public class GenerateResumeModel
    {
        public List<JobExperienceDto> JobExperiences{ get; set; }
        public List<EducationExperienceDto> EducationExperiences { get; set; }
        public UserCandidateProfileDto Candidate { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}
