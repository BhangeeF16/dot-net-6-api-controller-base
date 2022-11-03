#region Imports
using Domain.Common.DataAccessHelpers;
using Domain.Entities.GeneralModule;
using Domain.Entities.UsersModule;
using Domain.IRepositories.ICandidateRepositories;
using Domain.IRepositories.IGenericRepositories;
using Domain.IRepositories.IJobPostModule;
using Domain.IRepositories.IUsersModule;
using Infrastructure.DataAccess.EntityRepositories;
using Infrastructure.DataAccess.EntityRepositories.CandidateModule;
using Infrastructure.DataAccess.EntityRepositories.JobPostModule;
using Infrastructure.DataAccess.EntityRepositories.UserModule;
using Infrastructure.Persistence;

#endregion

namespace Infrastructure.DataAccess.GenericRepositories;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(ApplicationDbContext appDbContext, ConnectionInfo connectionInfo)
    {
        _context = appDbContext;
        _connectionInfo = connectionInfo;
    }

    #region Private member variables...

    private readonly ApplicationDbContext _context;
    private readonly ConnectionInfo _connectionInfo;
    private IUserRepository? _userRepository;
    private ICandidateRepository? _candidateRepository;
    private IPostRepository? _postRepository;
    private IJobRepository? _jobPostRepository;
    private IGenericRepository<UserRole>? _roleRepository;
    private IGenericRepository<MiddlewareLog>? _middlewareLogsRepository;
    private IGenericRepository<ApiCallLog>? _apiCallLogsRepository;
    private IGenericRepository<AppSetting>? _appsettingsRepository;

    #endregion

    #region Public Repository Creation properties...

    public ICandidateRepository CandidateRepository
    {
        get
        {
            if (_candidateRepository == null)
                _candidateRepository = new CandidateRepository(_context, _connectionInfo);
            return _candidateRepository;
        }
    }
    public IPostRepository PostRepository
    {
        get
        {
            if (_postRepository == null)
                _postRepository = new PostRepository(_context, _connectionInfo);
            return _postRepository;
        }
    }
    public IJobRepository JobRepository
    {
        get
        {
            if (_jobPostRepository == null)
                _jobPostRepository = new JobRepository(_context, _connectionInfo);
            return _jobPostRepository;
        }
    }
    public IUserRepository UserRepository
    {
        get
        {
            if (_userRepository == null)
                _userRepository = new UserRepository(_context, _connectionInfo);
            return _userRepository;
        }
    }
    public IGenericRepository<UserRole> RoleRepository
    {
        get
        {
            if (_roleRepository == null)
                _roleRepository = new GenericRepository<UserRole>(_context, _connectionInfo);
            return _roleRepository;
        }
    }
    public IGenericRepository<MiddlewareLog> MiddlewareLogsRepository
    {
        get
        {
            if (_middlewareLogsRepository == null)
                _middlewareLogsRepository = new GenericRepository<MiddlewareLog>(_context, _connectionInfo);
            return _middlewareLogsRepository;
        }
    }
    public IGenericRepository<ApiCallLog> ApiCallLogsRepository
    {
        get
        {
            if (_apiCallLogsRepository == null)
                _apiCallLogsRepository = new GenericRepository<ApiCallLog>(_context, _connectionInfo);
            return _apiCallLogsRepository;
        }
    }
    public IGenericRepository<AppSetting> AppsettingsRepository
    {
        get
        {
            if (_appsettingsRepository == null)
                _appsettingsRepository = new GenericRepository<AppSetting>(_context, _connectionInfo);
            return _appsettingsRepository;
        }
    }

    #endregion
    public int Complete()
    {
        return _context.SaveChanges();
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
    {
        if (disposing) _context.Dispose();
    }
}
