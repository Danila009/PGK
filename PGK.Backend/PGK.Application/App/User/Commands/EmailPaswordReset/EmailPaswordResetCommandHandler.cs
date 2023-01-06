using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Common;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Application.Security;
using PGK.Application.Services.EmailService;
using System.Net;

namespace PGK.Application.App.User.Commands.EmailPaswordReset
{
    public class EmailPaswordResetCommandHandler
        : IRequestHandler<EmailPaswordResetCommand, ContentResult>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IEmailService _emailService;
        private readonly IAuth _auth;

        public EmailPaswordResetCommandHandler(IPGKDbContext dbContext, IEmailService emailService,
            IAuth auth) => (_dbContext,_emailService, _auth) = (dbContext,emailService, auth);

        public async Task<ContentResult> Handle(EmailPaswordResetCommand request,
            CancellationToken cancellationToken)
        {
            var htmlContent = string.Empty;

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);

            if (user == null)
            {
                throw new NotFoundException(nameof(Domain.User.User), request.UserId);
            }

            if (user.SendEmailToken != request.Token)
            {
                throw new UnauthorizedAccessException($"Invalid token");
            }

            if (!_auth.TokenValidation(token: request.Token, type: TokenType.EMAIL_SEND_TOKEN))
            {
                throw new UnauthorizedAccessException("The token has expired");
            }

            var newPassword = Guid.NewGuid().ToString();

            var hashPassword = PasswordHash.CreateHash(newPassword);

            user.Password = hashPassword;
            user.SendEmailToken = null;
            await _dbContext.SaveChangesAsync(cancellationToken);

            var html = File.ReadAllText("Html/email_addres_password_reset_web.html");

            html = html.Replace("{{username}}", user.FirstName);
            html = html.Replace("{{password}}", newPassword);

            html = html.Replace("{{image_src}}", GetPgkIconUrl());

            if(user.Email != null)
            {
                var sentEmailMessageHtml = File.ReadAllText("Html/send_email_new_password.html");

                sentEmailMessageHtml = sentEmailMessageHtml.Replace("{{username}}", user.FirstName);
                
                sentEmailMessageHtml = sentEmailMessageHtml.Replace("{{image_src}}", GetPgkIconUrl());

                await _emailService.SendEmailAsync(
                    email: user.Email,
                    subject: "Пароль успешно изменен",
                    message: sentEmailMessageHtml);

                await _emailService.SendEmailAsync(
                    email: user.Email,
                    subject: "Пароль успешно изменен",
                    message: html);
            }

            return new ContentResult
            {
                ContentType = "text/html; charset=utf-8",
                Content = html,
                StatusCode = (int)HttpStatusCode.OK,
            };
        }

        private string GetPgkIconUrl()
        {
            var date = DateTime.Now.Date;

            if ((date.Month == 12 && date.Day >= 20) || (date.Month == 1 && date.Day <= 15))
            {
                return $"{Constants.BASE_URL}/Image/new_year_pgk_icon.png";
            }
            else
            {
                return $"{Constants.BASE_URL}/Image/pgk_icon.png";
            }
        }
    }
}
