using PGK.Domain.User;
using System.Security.Claims;

namespace PGK.Application.Security
{
    public interface IAuth
    {

        public string GetRefreshToken();

        public string CreateToken(string email, string userId,
            UserRole userRole);

        public Claim[] Claims(string email, string userId,
            UserRole userRole);
    }
}
