using Domain.Common.DataAccessHelpers;
using Domain.Entities.PostModule;
using Domain.IRepositories.IGenericRepositories;
using Domain.IRepositories.IJobPostModule;
using Infrastructure.DataAccess.GenericRepositories;
using Infrastructure.Persistence;

namespace Infrastructure.DataAccess.EntityRepositories.JobPostModule
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        #region Constructors and locals

        private readonly ApplicationDbContext _dbContext;
        private readonly ConnectionInfo _connectionInfo;
        public PostRepository(ApplicationDbContext context, ConnectionInfo connectionInfo) : base(context, connectionInfo)
        {
            _dbContext = context;
            _connectionInfo = connectionInfo;
        }

        #endregion

        #region Modular Repositories

        private IGenericRepository<PostComment>? _commentRepository;
        public IGenericRepository<PostComment> CommentRepository
        {
            get
            {
                if (_commentRepository == null)
                    _commentRepository = new GenericRepository<PostComment>(_context, _connectionInfo);
                return _commentRepository;
            }
        }

        private IGenericRepository<PostFile>? _fileRepository;
        public IGenericRepository<PostFile> FileRepository
        {
            get
            {
                if (_fileRepository == null)
                    _fileRepository = new GenericRepository<PostFile>(_context, _connectionInfo);
                return _fileRepository;
            }
        }

        private IGenericRepository<PostReaction>? _reactionRepository;
        public IGenericRepository<PostReaction> ReactionRepository
        {
            get
            {
                if (_reactionRepository == null)
                    _reactionRepository = new GenericRepository<PostReaction>(_context, _connectionInfo);
                return _reactionRepository;
            }
        }

        private IGenericRepository<PostShare>? _sShareRepository;
        public IGenericRepository<PostShare> ShareRepository
        {
            get
            {
                if (_sShareRepository == null)
                    _sShareRepository = new GenericRepository<PostShare>(_context, _connectionInfo);
                return _sShareRepository;
            }
        }

        private IGenericRepository<PostTag>? _tagRepository;
        public IGenericRepository<PostTag> TagRepository
        {
            get
            {
                if (_tagRepository == null)
                    _tagRepository = new GenericRepository<PostTag>(_context, _connectionInfo);
                return _tagRepository;
            }
        }

        private IGenericRepository<PostReactionType>? _reactionTypeRepository;
        public IGenericRepository<PostReactionType> ReactionTypeRepository
        {
            get
            {
                if (_reactionTypeRepository == null)
                    _reactionTypeRepository = new GenericRepository<PostReactionType>(_context, _connectionInfo);
                return _reactionTypeRepository;
            }
        }

        private IGenericRepository<PostType>? _postTypeRepository;
        public IGenericRepository<PostType> PostTypeRepository
        {
            get
            {
                if (_postTypeRepository == null)
                    _postTypeRepository = new GenericRepository<PostType>(_context, _connectionInfo);
                return _postTypeRepository;
            }
        }

        private IGenericRepository<PostCommentSetting>? _commentSettingRepository;
        public IGenericRepository<PostCommentSetting> CommentSettingRepository
        {
            get
            {
                if (_commentSettingRepository == null)
                    _commentSettingRepository = new GenericRepository<PostCommentSetting>(_context, _connectionInfo);
                return _commentSettingRepository;
            }
        }

        private IGenericRepository<PostViewSetting>? _viewSettingRepository;
        public IGenericRepository<PostViewSetting> ViewSettingRepository
        {
            get
            {
                if (_viewSettingRepository == null)
                    _viewSettingRepository = new GenericRepository<PostViewSetting>(_context, _connectionInfo);
                return _viewSettingRepository;
            }
        }

        #endregion
    }
}
