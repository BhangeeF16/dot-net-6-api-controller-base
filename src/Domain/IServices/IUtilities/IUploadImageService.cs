using Microsoft.AspNetCore.Http;

namespace Domain.IServices.IHelperServices
{
    public interface IUploadImageService
    {
        string GetMimeType(string fileName);
        string GetImageCompleteUrl(string Image);
        string UploadFile(IFormFile file, string DirectoryName = "DEFAULT");
        bool DeleteFile(string Image);
    }
}