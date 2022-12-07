using MediatR;
using Microsoft.AspNetCore.Mvc;
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
            var htmlContent = string.Empty;

            var user = await _dbContext.Users.FindAsync(request.UserId);

            if (user == null)
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
