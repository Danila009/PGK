using System.Security.Claims;

namespace PGK.Application.Security
{
    public interface IAuth
    {

        public string CreateRefreshToken();

        public string CreateToken(int userId,
            string userRole);

        public Claim[] Claims(int userId,
            string userRole);
    }
}
