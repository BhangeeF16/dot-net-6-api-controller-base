using Domain.Common.Models.UserModule;
using Domain.Entities.UsersModule;
using Domain.IServices.IAuthServices;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Pipeline.Authorization.JWT
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IDatetimeProvider _dateTimeProvider;
        private readonly JwtSettings _jwtSettings;
        public JwtTokenGenerator(IDatetimeProvider dateTimeProvider, IOptions<JwtSettings> jwtSettings)
        {
            _dateTimeProvider = dateTimeProvider;
            _jwtSettings = jwtSettings.Value;
        }

        public UserTokens GenerateToken(User thisUser)
        {
            try
            {
                var claimsList = new List<Claim>()
                {
                    new Claim(ClaimTypes.Sid, thisUser?.ID.ToString() ?? string.Empty),
                    new Claim(ClaimTypes.Name, thisUser?.FirstName ?? string.Empty),
                    new Claim(ClaimTypes.NameIdentifier, thisUser?.UserName ?? string.Empty),
                    new Claim(ClaimTypes.Email, thisUser?.UserName ?? string.Empty),
                    new Claim(ClaimTypes.Role, thisUser?.fk_RoleID.ToString() ?? string.Empty),
                };

                // using symmetricKey because we later need to authenticateit (not using IDENTITY SERVER)

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.IssuerSigningKey ?? string.Empty));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expiresIn = _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryAfterMinutes);

                var securityToken = new JwtSecurityToken(
                    issuer: _jwtSettings.ValidIssuer,
                    audience: _jwtSettings.ValidAudience,
                    expires: expiresIn,
                    claims: claimsList,
                    signingCredentials: creds);

                return new UserTokens()
                {
                    UserId = thisUser.ID,
                    FullName = $"{thisUser.FirstName} {thisUser.LastName}",
                    Email = thisUser.UserName,
                    UserName = thisUser.UserName,
                    RoleId = thisUser.fk_RoleID,
                    ExpiryTime = expiresIn,
                    Token = new JwtSecurityTokenHandler().WriteToken(securityToken)
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

//var header = new JwtHeader(creds);
//var payload = new JwtPayload(jwtSettings.IssuerSigningKey ?? String.Empty, jwtSettings.ValidAudience ?? String.Empty, claimsList, null, DateTime.Now.AddMinutes(30));
//var token = new JwtSecurityToken(header, payload);
//return new JwtSecurityTokenHandler().WriteToken(token);
