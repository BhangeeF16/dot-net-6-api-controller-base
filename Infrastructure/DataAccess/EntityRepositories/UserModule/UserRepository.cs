using Domain.Common.DataAccessHelpers;
using Domain.Common.Models.CorporateModule;
using Domain.Common.Models.UserModule;
using Domain.Entities.CorporateModule;
using Domain.Entities.UsersModule;
using Domain.IRepositories.IGenericRepositories;
using Domain.IRepositories.IUsersModule;
using Infrastructure.DataAccess.GenericRepositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.EntityRepositories.UserModule;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ConnectionInfo _connectionInfo;

    public UserRepository(ApplicationDbContext dbContext, ConnectionInfo connectionInfo) : base(dbContext, connectionInfo)
    {
        _dbContext = dbContext;
        _connectionInfo = connectionInfo;
    }

    #region Inner-Repositories

    private IGenericRepository<Corporate>? _corporateRepository;
    private IGenericRepository<UserCorporateProfile>? _userCorporateProfileRepository;
    public IGenericRepository<Corporate> CorporateRepository
    {
        get
        {
            if (_corporateRepository == null)
                _corporateRepository = new GenericRepository<Corporate>(_context, _connectionInfo);
            return _corporateRepository;
        }
    }
    public IGenericRepository<UserCorporateProfile> UserCorporateProfileRepository
    {
        get
        {
            if (_userCorporateProfileRepository == null)
                _userCorporateProfileRepository = new GenericRepository<UserCorporateProfile>(_context, _connectionInfo);
            return _userCorporateProfileRepository;
        }
    }

    #endregion

    #region Methods

    public UserCandidateProfile GetUserCandidateProfile(int UserId)
    {
        return _dbContext.UserCandidateProfiles.Include(x => x.CandidateResumeUploadDetails).FirstOrDefault(x => x.fk_UserID == UserId && x.IsActive == true && x.IsDeleted == false) ?? new UserCandidateProfile();
    }
    public UserCorporateProfile GetUserCorporateProfile(int UserId)
    {
        return _dbContext.Users.Include(x => x.CorporateProfile).FirstOrDefault(x => x.ID == UserId && x.IsActive == true && x.IsDeleted == false).CorporateProfile ?? new UserCorporateProfile();
    }
    public Corporate GetCorporateOfUser(int UserId)
    {
        var profile = _dbContext.Users.Include(x => x.CorporateProfile).FirstOrDefault(x => x.ID == UserId && x.IsActive == true && x.IsDeleted == false).CorporateProfile ?? new UserCorporateProfile();
        return _dbContext.Corporates.FirstOrDefault(x => x.ID == profile.fk_CorporateID && x.IsActive == true && x.IsDeleted == false) ?? new Corporate();
    }

    #endregion

    #region commment

    //public async Task<List<TResponse>> GetListOfCompanysUserAsync<TResponse>(int companyId) where TResponse : class
    //{
    //    var theseUsers = await ExecuteSqlStoredProcedureAsync<TResponse>(StoredProceduresLegend.GetCompanysUsers, new Pagination(), new List<SqlParameter>()
    //    {
    //        new SqlParameter("@companyId", companyId  )
    //    });
    //    return theseUsers.Items;
    //}

    //// Get All Users List From DB
    //public async Task<IList<UserEntity>> GetAllUsersList()
    //{
    //    try
    //    {
    //        var users = await _unitOfWork.UserRepository.GetAll();
    //        return users.ToList();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //}
    //// Get User Details By Id From DB 
    //public async Task<UserEntity> GetUserById(int id)
    //{
    //    try
    //    {
    //        var UserDetailsById = await _unitOfWork.UserRepository.GetFirstOrDefault(x => x.Id == id);
    //        return UserDetailsById;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //}
    //// Delete user From Database
    //public async Task<string> DeleteUser(int Id)
    //{
    //    try
    //    {
    //        if (Id > 0)
    //        {
    //            var userData = await _unitOfWork.UserRepository.Get(Id);
    //            _unitOfWork.UserRepository.Delete(userData);
    //        }
    //        return "User Is Successfully Deleted";
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //}
    //// Save User Details 
    //private async Task<UserEntity> CreateUser(UserEntity user)
    //{
    //    try
    //    {
    //        user.CreatedAt = DateTime.UtcNow;
    //        user.CreatedBy = user.FirstName + " " + user.LastName;

    //        await _unitOfWork.UserRepository.Add(user);
    //        _unitOfWork.Complete();
    //        return user;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //}
    //// Update User Details
    //private async Task<UserEntity> UpdateUser(UserEntity user)
    //{
    //    UserEntity UserResult;
    //    try
    //    {
    //        var UserDetails = await _unitOfWork.UserRepository.GetFirstOrDefault(x => x.Id == user.Id);
    //        UserResult = new UserEntity
    //        {
    //            Id = user.Id,
    //            Role = user.Role,
    //            CognitoUserId = user.CognitoUserId,
    //            FirstName = user.FirstName,
    //            LastName = user.LastName,
    //            Username = user.Username,
    //            Password = user.Password,
    //            Company = user.Company,
    //            DOB = user.DOB,
    //            PhoneNumber = user.PhoneNumber,
    //            Gander = user.Gander,
    //            Ethnicity = user.Ethnicity,
    //            Address = user.Address,
    //            ModifiedAt = DateTime.UtcNow,
    //            ModifiedBy = user.FirstName + " " + user.LastName,
    //        };
    //        await _unitOfWork.UserRepository.Add(UserResult);
    //        _unitOfWork.Complete();
    //        return UserResult;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //}
    //// Check User Exist function
    //public async Task<bool> IsExist(string UserName)
    //{
    //    try
    //    {
    //        var isExistUser = await _unitOfWork.UserRepository.GetFirstOrDefault(x => x.Username == UserName);
    //        if (isExistUser == null)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //}

    #endregion
}
