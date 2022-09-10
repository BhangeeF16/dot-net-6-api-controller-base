using Domain.Entities.UsersModule;
using Domain.IRepositories.IGenericRepositories;

namespace Domain.IRepositories.IEntityRepositories;

public interface IUserRepository : IGenericRepository<User>
{

}
