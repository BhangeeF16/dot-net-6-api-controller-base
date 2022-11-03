using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.RequestModels.CandidateModule
{
    public class UpsertCandidateInfoRequest
    {
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public string? JobTitle { get; set; }
        public string? FacecbookUserName { get; set; }
        public string? LinkedInUserName { get; set; }
        public string? About { get; set; }
    }
}
