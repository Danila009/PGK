using System.Security.Claims;

namespace PGK.Application.Security
{
    public interface IAuth
    {

        public string CreateToken();

        public bool TokenValidation(string token, TokenType type);

        public string CreateAccessToken(int userId,
            string userRole);

        public Claim[] Claims(int userId,
            string userRole);
    }
}
