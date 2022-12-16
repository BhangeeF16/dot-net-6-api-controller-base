#region Imports

using Domain.Entities.GeneralModule;
using Domain.Entities.UsersModule;
using Domain.IRepositories.IEntityRepositories;

#endregion

namespace Domain.IRepositories.IGenericRepositories;

public interface IUnitOfWork : IDisposable
{
    int Complete();

    IUserRepository UserRepository { get; }
    IGenericRepository<UserRole> RoleRepository { get; }

    IGenericRepository<MiddlewareLog> MiddlewareLogsRepository { get; }
    IGenericRepository<ApiCallLog> ApiCallLogsRepository { get; }
    IGenericRepository<AppSetting> AppsettingsRepository { get; }
}
