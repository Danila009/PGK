using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PGK.Application.App.User.Commands.EmailVerification
{
    public class EmailVerificationCommand : IRequest<ContentResult>
    {
        public int UserId { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
