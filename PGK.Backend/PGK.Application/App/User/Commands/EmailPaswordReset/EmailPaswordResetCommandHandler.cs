using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Common;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Application.Security;
using System.Net;

namespace PGK.Application.App.User.Commands.EmailPaswordReset
{
    public class EmailPaswordResetCommandHandler
        : IRequestHandler<EmailPaswordResetCommand, ContentResult>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IAuth _auth;

        public EmailPaswordResetCommandHandler(IPGKDbContext dbContext,
            IAuth auth) => (_dbContext, _auth) = (dbContext, auth);

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

            htmlContent = $"<h1>New passoword: {newPassword}</h1>";

            return new ContentResult
            {
                ContentType = "text/html",
                Content = htmlContent,
                StatusCode = (int)HttpStatusCode.OK,
            };
        }
    }
}
