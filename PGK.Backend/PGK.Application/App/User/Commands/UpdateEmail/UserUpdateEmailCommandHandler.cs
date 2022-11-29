using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Application.Services.EmailService;

namespace PGK.Application.App.User.Commands.UpdateEmail
{
    public class UserUpdateEmailCommandHandler
        : IRequestHandler<UserUpdateEmailCommand>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IEmailService _emailService;

        public UserUpdateEmailCommandHandler(IPGKDbContext dbContext,
            IEmailService emailService) => (_dbContext, _emailService) = (dbContext, emailService);

        public async Task<Unit> Handle(UserUpdateEmailCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(request.UserId);

            if(user == null)
            {
                throw new NotFoundException(nameof(Domain.User.User), request.UserId);
            }

            var sendEmailToken = $"update_email_{Guid.NewGuid()}";

            user.Email = request.Email;
            user.EmailVerification = false;
            user.SendEmailToken = sendEmailToken;
            await _dbContext.SaveChangesAsync(cancellationToken);

            await _emailService.SendEmailAsync(
                email: user.Email,
                subject: "Подтверждение адреса электронной почты для входа в приложение ПГК",
                message: $"<h1>https://localhost:7002/api/User/{user.Id}/Email/Verification/{sendEmailToken}</h1>");



            return Unit.Value;
        }
    }
}
