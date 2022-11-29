using MediatR;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using System.Net;

namespace PGK.Application.App.User.Commands.EmailVerification
{
    public class EmailVerificationCommandHandler
        : IRequestHandler<EmailVerificationCommand, ContentResult>
    {
        private readonly IPGKDbContext _dbContext;

        public EmailVerificationCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<ContentResult> Handle(EmailVerificationCommand request,
            CancellationToken cancellationToken)
        {
            var htmlContent = string.Empty;

            var user = await _dbContext.Users.FindAsync(request.UserId);

            if(user == null)
            {
                throw new NotFoundException(nameof(Domain.User.User), request.UserId);
            }

            if (user.EmailVerification)
            {
                htmlContent = "<h1>Email уже подвержден</h1>";

                return new ContentResult
                {
                    ContentType = "text/html",
                    Content = htmlContent,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                };
            }

            if (user.SendEmailToken != request.Token || user.SendEmailToken.StartsWith("email_verification_"))
            {
                throw new UnauthorizedAccessException();
            }

            user.EmailVerification = true;
            user.SendEmailToken = null;
            await _dbContext.SaveChangesAsync(cancellationToken);

            htmlContent = $"<h1>{user.EmailVerification}</h1>";

            return new ContentResult
            {
                ContentType = "text/html",
                Content = htmlContent,
                StatusCode = (int)HttpStatusCode.OK,
            };
        }
    }
}
