#region Imports
using Domain.Common.DataAccessHelpers;
using Domain.Entities.GeneralModule;
using Domain.Entities.UsersModule;
using Domain.IRepositories.IEntityRepositories;
using Domain.IRepositories.IGenericRepositories;
using Infrastructure.DataAccess.EntityRepositories;
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
    private IGenericRepository<UserRole>? _roleRepository;
    private IGenericRepository<MiddlewareLogs>? _middlewareLogsRepository;
    private IGenericRepository<ApiCallLogs>? _apiCallLogsRepository;
    private IGenericRepository<AppSetting>? _appsettingsRepository;

    #endregion

    #region Public Repository Creation properties...

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
    public IGenericRepository<MiddlewareLogs> MiddlewareLogsRepository
    {
        get
        {
            if (_middlewareLogsRepository == null)
                _middlewareLogsRepository = new GenericRepository<MiddlewareLogs>(_context, _connectionInfo);
            return _middlewareLogsRepository;
        }
    }
    public IGenericRepository<ApiCallLogs> ApiCallLogsRepository
    {
        get
        {
            if (_apiCallLogsRepository == null)
                _apiCallLogsRepository = new GenericRepository<ApiCallLogs>(_context, _connectionInfo);
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
