using Domain.Common.DataAccessHelpers;
using Domain.Entities.GeneralModule;
using Domain.IRepositories.IGeneralModule;
using Infrastructure.DataAccess.GenericRepositories;
using Infrastructure.Persistence;

namespace Infrastructure.DataAccess.EntityRepositories.GeneralModule;

public class AppSettingsRepository : GenericRepository<AppSetting>, IAppSettingsRepository
{
    private readonly ApplicationDbContext _dbContext;
    public AppSettingsRepository(ApplicationDbContext dbContext, ConnectionInfo connectionInfo) : base(dbContext, connectionInfo)
    {
        _dbContext = dbContext;
    }
    public Task<string> UpsertAppSettings(AppSetting appSetting)
    {
        if (appSetting.Id == 0)
        {
            var createData = AddAsync(appSetting);
            return Task.FromResult("User is Successfully saved");
        }
        else
        {
            Update(appSetting);
            return Task.FromResult("User is Updated Successfully ");
        }
    }
    #region commment

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
