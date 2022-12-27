using MediatR;

namespace PGK.Application.App.User.Queries.GetTelegramToken
{
    public class GetTelegramTokenQuery : IRequest<string>
    {
        public int UserId { get; set; }
    }
}
