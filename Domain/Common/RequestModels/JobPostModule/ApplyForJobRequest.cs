using Microsoft.AspNetCore.Http;

namespace Domain.Common.RequestModels.JobPostModule
{
    public class ApplyForJobRequest
    {
        public int JobID { get; set; }
        public int? ResumeID { get; set; } = 0;
        public IFormFile? File { get; set; }
    }
}
