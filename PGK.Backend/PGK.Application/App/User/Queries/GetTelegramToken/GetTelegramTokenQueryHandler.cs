using MediatR;
using PGK.Application.Interfaces;
using PGK.Application.Common.Exceptions;
using PGK.Application.Security;

namespace PGK.Application.App.User.Queries.GetTelegramToken
{
    internal class GetTelegramTokenQueryHandler
        : IRequestHandler<GetTelegramTokenQuery, string>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IAuth _auth;

        public GetTelegramTokenQueryHandler(IPGKDbContext dbContext, IAuth auth) =>
            (_dbContext, _auth) = (dbContext, auth);

        public async Task<string> Handle(GetTelegramTokenQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(request.UserId);

            if(user == null)
            {
                throw new NotFoundException(nameof(Domain.User.User), request.UserId);
            }

            var token = _auth.CreateToken();

            user.TelegramToken = token;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return token;
        }
    }
}
