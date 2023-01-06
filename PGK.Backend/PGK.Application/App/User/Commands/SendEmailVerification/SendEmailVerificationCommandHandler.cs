using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Interfaces;
using PGK.Application.Services.EmailService;
using PGK.Application.Common.Exceptions;
using PGK.Application.Common;
using PGK.Application.Security;

namespace PGK.Application.App.User.Commands.SendEmailVerification
{
    public class SendEmailVerificationCommandHandler
        :IRequestHandler<SendEmailVerificationCommand>
    {

        private readonly IPGKDbContext _dbContext;
        private readonly IEmailService _emailService;
        private readonly IAuth _auth;

        public SendEmailVerificationCommandHandler(IPGKDbContext dbContext,
            IEmailService emailService, IAuth auth) =>
            (_dbContext, _emailService, _auth) = (dbContext, emailService, auth);

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

            var token = _auth.CreateToken();

            user.SendEmailToken = token;
            await _dbContext.SaveChangesAsync(cancellationToken);

            var date = DateTime.Now.Date;

            var html = File.ReadAllText("Html/send_email_verification_message.html");

            html = html.Replace("{{username}}", user.FirstName);
            html = html.Replace("{{email}}", user.Email);
            html = html.Replace("{{url}}", $"{Constants.BASE_URL}/User/{user.Id}/Email/Pasword/Reset.html?token={token}");

            if ((date.Month == 12 && date.Day >= 20) || (date.Month == 1 && date.Day <= 15))
            {
                html = html.Replace("{{image_src}}", $"{Constants.BASE_URL}/Image/new_year_pgk_icon.png");
            }
            else
            {
                html = html.Replace("{{image_src}}", $"{Constants.BASE_URL}/Image/pgk_icon.png");
            }

            await _emailService.SendEmailAsync(
                email: user.Email,
                subject: "Подтверждение адреса электронной почты для входа в приложение ПГК",
                message: html);

            return Unit.Value;
        }
    }
}
