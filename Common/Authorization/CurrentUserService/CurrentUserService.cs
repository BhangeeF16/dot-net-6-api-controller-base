using Domain.IServices.IAuthServices;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
namespace Common.Authorization.CurrentUserService
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly List<Claim> claims;
        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            claims = contextAccessor?.HttpContext?.User?.Identities?.First()?.Claims?.ToList() ?? new List<Claim>();
        }
        public int UserID
        {
            get
            {
                var UserId = claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid, StringComparison.OrdinalIgnoreCase))?.Value ?? string.Empty;
                return Convert.ToInt32(String.IsNullOrEmpty(UserId) ? 0 : UserId);
            }
        }
        public string FirstName
        {
            get
            {
                return claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name, StringComparison.OrdinalIgnoreCase))?.Value ?? string.Empty;
            }
        }
        public string UserName
        {
            get
            {
                return claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase))?.Value ?? string.Empty;
            }
        }
        public string RoleId
        {
            get
            {
                return claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role, StringComparison.OrdinalIgnoreCase))?.Value ?? string.Empty;
            }
        }
    }
}
