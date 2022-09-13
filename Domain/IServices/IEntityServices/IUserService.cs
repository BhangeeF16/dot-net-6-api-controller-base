using Domain.Common.DTO.UsersModule;
using Domain.Common.RequestModels.UserRequests;
using Domain.Common.ResponseModels;

namespace Domain.IServices.IEntityServices
{
    public interface IUserService
    {
        Task<UserLoginResponseModel> LoginRequestAsync(string Email, string Password);
        Task<RegisterRequestModel> RegisterRequestAsync(RegisterRequestModel model);
        Task<UserLoginResponseModel> ForgetPasswordRequestAsync(string Email);
        Task<UserLoginResponseModel> ChangePasswordAsync(UpdatePasswordRequestModel request);
        Task<UserDto> GetRequestAsync(int id);
        Task<UserDto> GetCurrentUserRequestAsync();
        Task<UserDto> AddRequestAsync(UpsertUserRequest model);
        Task<UserDto> UpdateRequestAsync(UpsertUserRequest model);
        Task<UserDto> UpdateCurrentUserRequestAsync(UpdateCurrentUserRequest model, string UploadedImage);
        Task<bool> DeleteRequestAsync(int id);
        Task<bool> DoesExistAsync(int Id);
        Task<bool> DoesExistAsync(string Email);
    }
}
