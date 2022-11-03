using Domain.Entities.CorporateModule;
using Domain.Entities.UsersModule;
using Domain.IRepositories.IGenericRepositories;

namespace Domain.IRepositories.IUsersModule;

public interface IUserRepository : IGenericRepository<User>
{
    IGenericRepository<Corporate> CorporateRepository { get; }
    IGenericRepository<UserCorporateProfile> UserCorporateProfileRepository { get; }

    Corporate GetCorporateOfUser(int UserId);
    UserCandidateProfile GetUserCandidateProfile(int UserId);
    UserCorporateProfile GetUserCorporateProfile(int UserId);

}
