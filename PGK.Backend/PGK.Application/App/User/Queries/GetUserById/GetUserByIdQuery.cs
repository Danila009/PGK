using MediatR;
using PGK.Application.App.User.Queries.GetUserLits;

namespace PGK.Application.App.User.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public int UserId { get; set; }
    }
}
