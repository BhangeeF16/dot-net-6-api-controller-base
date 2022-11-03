namespace Domain.Common.RequestModels.JobPostModule
{
    public class AddCommentRequest
    {
        public int PostID { get; set; }
        public string? Comment { get; set; }
    }
}
