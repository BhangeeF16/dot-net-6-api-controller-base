using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Models.JobPostModule
{
    public class JobForJobsPage
    {
        public int JobID { get; set; }
        public int PostID { get; set; }
        public int fk_CorporateID { get; set; }
        public int fk_JobPostedByProfileID { get; set; }

        public int Comments { get; set; }
        public int Reactions { get; set; }
        public int Applications { get; set; }
        
        public DateTime? CreatedAt { get; set; }
        
        public string? Company { get; set; }
        public string? HeadQuarterName { get; set; }
        public string? Caption { get; set; }
        public string? PostTags { get; set; }
        public string? PostFiles { get; set; }
        public int JobType { get; set; }
        public int WorkPlaceType { get; set; }
        public string? Location { get; set; }
    }
}
