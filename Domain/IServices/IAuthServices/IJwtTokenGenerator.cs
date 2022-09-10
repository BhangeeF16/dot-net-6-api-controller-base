using Domain.Common.AuthModels;
using Domain.Entities.UsersModule;

namespace Domain.IServices.IAuthServices
{
    public interface IJwtTokenGenerator
    {
        UserTokens GenerateToken(User thisUser);
    }
}
