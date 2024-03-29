﻿using Domain.IServices.IAuthServices;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
namespace Application.Pipeline.Authorization.CurrentUserService
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly List<Claim> claims;
        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            claims = contextAccessor?.HttpContext?.User?.Identities?.First()?.Claims?.ToList() ?? new List<Claim>();
        }

        public int ID
        {
            get
            {
                return Convert.ToInt32(claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid, StringComparison.OrdinalIgnoreCase))?.Value);
            }
        }
        public string Email
        {
            get
            {
                return claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email, StringComparison.OrdinalIgnoreCase))?.Value ?? string.Empty;
            }
        }
        public string FirstName
        {
            get
            {
                return claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name, StringComparison.OrdinalIgnoreCase))?.Value ?? string.Empty;
            }
        }
        public int RoleID
        {
            get
            {
                return Convert.ToInt32(claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role, StringComparison.OrdinalIgnoreCase))?.Value);
            }
        }
    }
}
