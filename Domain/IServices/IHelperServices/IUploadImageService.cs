using Microsoft.AspNetCore.Http;

namespace Domain.IServices.IHelperServices
{
    public interface IUploadImageService
    {
        string GetMimeType(string fileName);
        string GetImageCompleteUrl(string BaseUrl, string Image);
        string UploadImage(IFormFile file, string BaseUrl, string physicalPath);
        bool DeleteAttachment(string BaseUrl, string Image);
    }
}