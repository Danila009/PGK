using MediatR;

namespace PGK.Application.App.User.Queries.GetUserPhoto
{
    public class GetUserPhotoQuery : IRequest<byte[]>
    {
        public int UserId { get; set; }
    }
}
