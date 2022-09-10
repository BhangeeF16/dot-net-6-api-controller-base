using Domain.Common.DTO;
using Domain.Common.RequestModels;
using Domain.Common.ResponseModels;

namespace Domain.IServices.IEntityServices
{
    public interface IUserService
    {
        Task<UserLoginResponseModel> LoginRequestAsync(string Email, string Password);
        Task<RegisterRequestModel> RegisterRequestAsync(RegisterRequestModel model);
        Task<UserLoginResponseModel> ForgetPasswordRequestAsync(string Email);
        Task<UserLoginResponseModel> ChangePasswordAsync(ChangePasswordRequestModel request);
        Task<UserDto> GetRequestAsync(int id);
        Task<UserDto> GetCurrentUserRequestAsync();
        Task<UserDto> AddRequestAsync(UserDto model);
        Task<UserDto> UpdateRequestAsync(UserDto model);
        Task<UserDto> UpdateCurrentUserRequestAsync(UserUpdateRequest model, string UploadedImage);
        Task<bool> DeleteRequestAsync(int id);
        Task<bool> DoesExistAsync(int Id);
        Task<bool> DoesExistAsync(string Email);
    }
}
