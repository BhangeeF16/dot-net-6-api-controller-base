using Microsoft.AspNetCore.Http;

namespace Domain.IServices.IHelperServices
{
    public interface IFileUploadService
    {
        string GetMimeType(string fileName);
        string GetFileCompleteUrl(string Image);
        string UploadFile(IFormFile file, string DirectoryName = "DEFAULT");
        bool DeleteFile(string Image);
    }
}