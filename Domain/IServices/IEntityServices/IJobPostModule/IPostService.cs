using Domain.Common.RequestModels.JobPostModule;

namespace Domain.IServices.IEntityServices.IJobPostModule
{
    public interface IPostService
    {
        Task<bool> CreatePostAsync(CreatePostRequest request);
        Task<bool> ReactToPostAsync(ReactToPostRequest request);
        Task<bool> AddCommentAsync(AddCommentRequest request);

    }
}
