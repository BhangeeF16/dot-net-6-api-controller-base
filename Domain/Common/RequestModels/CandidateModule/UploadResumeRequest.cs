using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Common.RequestModels.CandidateModule
{
    public class UploadResumeRequest
    {
        [FromForm]
        public IFormFile? File { get; set; }
    }
}
