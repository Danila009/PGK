using MediatR;

namespace PGK.Application.App.User.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserDetailsDto>
    {
        public int UserId { get; set; }
    }
}
