using Domain.IServices.IHelperServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using System.Net.Http.Headers;

namespace Common.HelperServices
{
    public class UploadImageService : IUploadImageService
    {
        public const string FolderName = "wwwroot//Uploads";
        public UploadImageService()
        {
            if (!Directory.Exists(FolderName))
            {
                Directory.CreateDirectory(FolderName);
            }
        }
        public string GetMimeType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(fileName, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
        public string GetImageCompleteUrl(string BaseUrl, string Image)
        {
            return $"{BaseUrl}//{Image}".Replace("\\", "//");
        }
        public string UploadImage(IFormFile file, string BaseUrl, string physicalPath)
        {
            var pathToSave = $"{physicalPath}//{FolderName}";
            var responsePath = $"{BaseUrl}//{FolderName}";

            if (file.Length > 0)
            {
                var existingFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString().Replace("\"", "");
                var fileExtension = Path.GetExtension(existingFileName);

                var fileName = existingFileName?.Replace(existingFileName, Guid.NewGuid().ToString());
                fileName = fileName.Insert(fileName.Length, fileExtension);

                var fullPath = $"{pathToSave}//{fileName}";
                UploadFile(fullPath, file);

                var filePathUsedToDisplay = $"{responsePath}//{fileName}";
                var filePathToSaveInDB = $"Uploads//{fileName}";
                return filePathToSaveInDB;
            }
            return string.Empty;
        }
        public bool DeleteAttachment(string BaseUrl, string Image)
        {
            var pathToDelete = Path.Combine(BaseUrl, "wwwroot");
            var fullPathUrl = Path.Combine(pathToDelete, Image);
            var file = new FileInfo(fullPathUrl);
            if (file.Exists)
            {
                file.Delete();
                return true;
            }
            else
            {
                return false;
            }
        }
        private static bool UploadFile(string savingPath, IFormFile file)
        {
            var check = false;
            using (var stream = new FileStream(savingPath.Replace("//", "\\"), FileMode.Create))
            {
                file.CopyTo(stream);
                check = true;
            }
            return check;
        }
    }
}
