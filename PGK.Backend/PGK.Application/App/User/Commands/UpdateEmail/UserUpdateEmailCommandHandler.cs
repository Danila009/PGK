using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Application.Security;
using PGK.Application.Services.EmailService;

namespace PGK.Application.App.User.Commands.UpdateEmail
{
    public class UserUpdateEmailCommandHandler
        : IRequestHandler<UserUpdateEmailCommand>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IEmailService _emailService;
        private readonly IAuth _auth;

        public UserUpdateEmailCommandHandler(IPGKDbContext dbContext,
            IEmailService emailService, IAuth auth) =>
            (_dbContext, _emailService, _auth) = (dbContext, emailService, auth);

        public async Task<Unit> Handle(UserUpdateEmailCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(request.UserId);

            if(user == null)
            {
                throw new NotFoundException(nameof(Domain.User.User), request.UserId);
            }

            var sendEmailToken = _auth.CreateToken();

            user.Email = request.Email;
            user.EmailVerification = false;
            user.SendEmailToken = sendEmailToken;
            await _dbContext.SaveChangesAsync(cancellationToken);

            await _emailService.SendEmailAsync(
                email: user.Email,
                subject: "Подтверждение адреса электронной почты для входа в приложение ПГК",
                message: $"<h1>{Common.Constants.BASE_URL}/User/{user.Id}/Email/Verification/{sendEmailToken}.html</h1>");



            return Unit.Value;
        }
    }
}
