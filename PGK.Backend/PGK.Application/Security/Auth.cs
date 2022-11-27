using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PGK.Domain.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PGK.Application.Security
{
    public class Auth : IAuth
    {
        private readonly IConfiguration _configuration;

        public Auth(IConfiguration configuration) => _configuration = configuration;

        public string CreateRefreshToken()
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            
            byte[] key = Guid.NewGuid().ToByteArray();
            
            string token = Convert.ToBase64String(time.Concat(key).ToArray());

            return token;
        }

        public string CreateToken(
            int userId, string userRole)
        {
            var claims = Claims(userRole: userRole,
                userId: userId);

            var key = GetSymmetricSecurityKey(_configuration["token:key"]);

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    audience: _configuration["token:audience"],
                    issuer: _configuration["token:issuer"],
                    notBefore: now,
                    claims: claims,
                    expires: now.Add(TimeSpan.FromMinutes(int.Parse(_configuration["token:accessTokenExpiryMinutes"]))),
                    signingCredentials: signIn);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public static SymmetricSecurityKey GetSymmetricSecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }

        public Claim[] Claims(int userId, string userRole)
        {
            return new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["token:subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, userRole),
                new Claim("user_id", userId.ToString())
            };
        }
    }
}
