using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Domain.RequestModels.UserRequests
{
    public class UpsertProfilePictureRequest
    {
        [FromForm]
        public IFormFile? ProfilePicture { get; set; }
    }
}
