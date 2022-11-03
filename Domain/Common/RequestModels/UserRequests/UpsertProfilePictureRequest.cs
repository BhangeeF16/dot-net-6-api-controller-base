using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Common.RequestModels.UserRequests
{
    public class UpsertProfilePictureRequest
    {
        [FromForm]
        public IFormFile? ProfilePicture { get; set; }
    }
}
