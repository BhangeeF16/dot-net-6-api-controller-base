using Domain.Entities.UsersModule;
using Domain.Models.AuthModels;

namespace Domain.IServices.IAuthServices
{
    public interface IJwtTokenGenerator
    {
        UserTokens GenerateToken(User thisUser);
    }
}
