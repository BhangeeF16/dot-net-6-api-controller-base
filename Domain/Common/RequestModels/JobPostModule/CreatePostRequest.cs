using Microsoft.AspNetCore.Http;

namespace Domain.Common.RequestModels.JobPostModule
{
    public class CreatePostRequest
    {
        public string? Text { get; set; }
        public int fk_CommentSettingID { get; set; }
        public int fk_PostViewSettingID { get; set; }
        public int fk_PostTypeID { get; set; }

        public List<string>? Tags { get; set; }
        public IFormFileCollection? Files { get; set; }
    }
}
