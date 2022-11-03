using Domain.Common.Models.UserModule;
using Domain.Entities.UsersModule;

namespace Domain.IServices.IAuthServices
{
    public interface IJwtTokenGenerator
    {
        UserTokens GenerateToken(User thisUser);
    }
}
