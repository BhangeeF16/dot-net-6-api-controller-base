using Domain.Entities.PostModule;
using Domain.IRepositories.IGenericRepositories;

namespace Domain.IRepositories.IJobPostModule
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        IGenericRepository<PostComment> CommentRepository { get; }
        IGenericRepository<PostFile> FileRepository { get; }
        IGenericRepository<PostReaction> ReactionRepository { get; }
        IGenericRepository<PostShare> ShareRepository { get; }
        IGenericRepository<PostTag> TagRepository { get; }

        IGenericRepository<PostReactionType> ReactionTypeRepository { get; }
        IGenericRepository<PostType> PostTypeRepository { get; }

        IGenericRepository<PostCommentSetting> CommentSettingRepository { get; }
        IGenericRepository<PostViewSetting> ViewSettingRepository { get; }
    }
}
