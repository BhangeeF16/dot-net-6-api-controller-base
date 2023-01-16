using Domain.Models.UsersModule;
using Domain.RequestModels.UserRequests;
using Domain.ResponseModels.UserResponses;

namespace Domain.IServices.IEntityServices.IUserModule
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

        Task<bool> StatusUpdateAsync(int id, bool status);
        Task<UserDto> UpdateRequestAsync(UpsertUserRequest model);
        Task<UserDto> UpdateCurrentUserRequestAsync(UpdateCurrentUserRequest model);
        Task<UserDto> UpdateProfilePictureRequestAsync(UpsertProfilePictureRequest model);

        Task<bool> DeleteRequestAsync(int id);


        Task<bool> DoesExistAsync(int Id);
        Task<bool> DoesExistAsync(string Email);
    }
}
