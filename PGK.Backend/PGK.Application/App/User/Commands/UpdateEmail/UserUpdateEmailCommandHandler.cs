using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Common;
using PGK.Application.Common.Exceptions;
using PGK.Application.Common.Model;
using PGK.Application.Interfaces;
using PGK.Application.Security;
using PGK.Application.Services.EmailService;

namespace PGK.Application.App.User.Commands.UpdateEmail
{
    public class UserUpdateEmailCommandHandler
        : IRequestHandler<UserUpdateEmailCommand, MessageModel>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IEmailService _emailService;
        private readonly IAuth _auth;

        public UserUpdateEmailCommandHandler(IPGKDbContext dbContext,
            IEmailService emailService, IAuth auth) =>
            (_dbContext, _emailService, _auth) = (dbContext, emailService, auth);

        public async Task<MessageModel> Handle(UserUpdateEmailCommand request,
            CancellationToken cancellationToken)
        {
            var userByEmail = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if(userByEmail != null)
            {
                return new MessageModel
                {
                    Message = "Пользователь с таким email уже сущестует"
                };
            }

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

            var date = DateTime.Now.Date;

            var html = File.ReadAllText("Html/send_email_verification_message.html");

            html = html.Replace("{{username}}", user.FirstName);
            html = html.Replace("{{email}}", user.Email);
            html = html.Replace("{{url}}", $"{Constants.BASE_URL}/User/{user.Id}/Email/Pasword/Reset.html?token={sendEmailToken}");

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

            return new MessageModel
            {
                Message = "Почта сохранена, мы отправили письмо для подтверждения почты"
            };
        }
    }
}
