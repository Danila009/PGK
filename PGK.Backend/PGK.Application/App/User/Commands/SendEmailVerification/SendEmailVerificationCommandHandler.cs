using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Interfaces;
using PGK.Application.Services.EmailService;
using PGK.Application.Common.Exceptions;

namespace PGK.Application.App.User.Commands.SendEmailVerification
{
    public class SendEmailVerificationCommandHandler
        :IRequestHandler<SendEmailVerificationCommand>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IEmailService _emailService;

        public SendEmailVerificationCommandHandler(IPGKDbContext dbContext,
            IEmailService emailService) => (_dbContext, _emailService) = (dbContext, emailService);

        public async Task<Unit> Handle(SendEmailVerificationCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);

            if(user == null)
            {
                throw new NotFoundException(nameof(Domain.User.User), request.UserId);
            }

            if(user.Email == null)
            {
                throw new Exception("user email null");
            }

            var token = $"email_verification_{Guid.NewGuid()}";

            user.SendEmailToken = token;
            await _dbContext.SaveChangesAsync(cancellationToken);

            await _emailService.SendEmailAsync(
                email: user.Email,
                subject: "Подтверждение адреса электронной почты для входа в приложение ПГК",
                message: $"<h1>https://localhost:7002/api/User/{user.Id}/Email/Verification/{token}</h1>");

            return Unit.Value;
        }
    }
}
