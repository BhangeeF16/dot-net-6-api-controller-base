#region Imports

using Domain.Entities.GeneralModule;
using Domain.Entities.UsersModule;
using Domain.IRepositories.ICandidateRepositories;
using Domain.IRepositories.IJobPostModule;
using Domain.IRepositories.IUsersModule;

#endregion

namespace Domain.IRepositories.IGenericRepositories;

public interface IUnitOfWork : IDisposable
{
    int Complete();

    IUserRepository UserRepository { get; }
    IGenericRepository<UserRole> RoleRepository { get; }

    ICandidateRepository CandidateRepository { get; }

    IPostRepository PostRepository { get; }
    IJobRepository JobRepository { get; }

    IGenericRepository<MiddlewareLog> MiddlewareLogsRepository { get; }
    IGenericRepository<ApiCallLog> ApiCallLogsRepository { get; }
    IGenericRepository<AppSetting> AppsettingsRepository { get; }
}
