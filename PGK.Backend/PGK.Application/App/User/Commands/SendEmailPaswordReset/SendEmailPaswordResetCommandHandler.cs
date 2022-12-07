using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Application.Services.EmailService;
using PGK.Application.Common;
using PGK.Application.Security;

namespace PGK.Application.App.User.Commands.SendEmailPaswordReset
{
    public class SendEmailPaswordResetCommandHandler
        : IRequestHandler<SendEmailPaswordResetCommand>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IEmailService _emailService;
        private readonly IAuth _auth;

        public SendEmailPaswordResetCommandHandler(IPGKDbContext dbContext,
            IEmailService emailService, IAuth auth) =>
            (_dbContext, _emailService, _auth) = (dbContext, emailService, auth);

        public async Task<Unit> Handle(SendEmailPaswordResetCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                throw new NotFoundException(nameof(Domain.User.User), request.Email);
            }

            if (user.Email == null)
            {
                throw new Exception("user email null");
            }

            var token = _auth.CreateToken();

            user.SendEmailToken = token;
            await _dbContext.SaveChangesAsync(cancellationToken);

            await _emailService.SendEmailAsync(
                email: user.Email,
                subject: "Изминить пароль в приложение пгк",
                message: $"<h1>{Constants.BASE_URL}/User/{user.Id}/Email/Pasword/Reset/{token}.html</h1>");

            return Unit.Value;
        }
    }
}
