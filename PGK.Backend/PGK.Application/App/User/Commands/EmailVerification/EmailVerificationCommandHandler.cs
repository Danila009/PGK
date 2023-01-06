using MediatR;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.Common;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Application.Security;
using System.Net;

namespace PGK.Application.App.User.Commands.EmailVerification
{
    public class EmailVerificationCommandHandler
        : IRequestHandler<EmailVerificationCommand, ContentResult>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IAuth _auth;

        public EmailVerificationCommandHandler(IPGKDbContext dbContext,
            IAuth auth) => (_dbContext, _auth) = (dbContext, auth);

        public IAuth Auth => _auth;

        public async Task<ContentResult> Handle(EmailVerificationCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(request.UserId);

            if (user == null)
            {
                throw new NotFoundException(nameof(Domain.User.User), request.UserId);
            }

            if (user.EmailVerification)
            {
                var htmlContent = File.ReadAllText("Html/user_message.html");

                htmlContent = htmlContent.Replace("{{username}}", user.FirstName);
                htmlContent = htmlContent.Replace("{{message}}", "Email уже подвержден");

                htmlContent = htmlContent.Replace("{{image_src}}", GetPgkIconUrl());

                return new ContentResult
                {
                    ContentType = "text/html; charset=utf-8",
                    Content = htmlContent,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                };
            }

            if (user.SendEmailToken != request.Token)
            {
                throw new UnauthorizedAccessException($"Invalid token");
            }

            if (!Auth.TokenValidation(token: request.Token, type: TokenType.EMAIL_SEND_TOKEN))
            {
                throw new UnauthorizedAccessException("The token has expired");
            }

            user.EmailVerification = true;
            user.SendEmailToken = null;
            await _dbContext.SaveChangesAsync(cancellationToken);

            var html = File.ReadAllText("Html/email_addres_verification_web.html");

            html = html.Replace("{{username}}", user.FirstName);

            html = html.Replace("{{image_src}}", GetPgkIconUrl());

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
