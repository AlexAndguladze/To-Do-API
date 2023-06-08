using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ToDo.API.Infrastructure.Auth.JWT
{
    public static class JWTHelper
    {
        public static string GenerateSecurityToken(string userId, IOptions<JWTConfiguration> options)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(options.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    //new Claim(ClaimTypes.Name, userName)
                    new Claim("userId", userId)
                }),
                Expires = DateTime.UtcNow.AddMinutes(options.Value.ExpirationInMinutes),
                Audience = "localhost",
                Issuer = "localhost",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
