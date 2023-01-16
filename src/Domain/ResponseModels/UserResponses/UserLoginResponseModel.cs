using Domain.Entities.UsersModule;

namespace Domain.ResponseModels.UserResponses
{
    public class UserLoginResponseModel
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public User? User { get; set; }
    }
}