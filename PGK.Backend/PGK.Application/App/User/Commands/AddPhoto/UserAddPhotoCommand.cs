using MediatR;
using Microsoft.AspNetCore.Http;

namespace PGK.Application.App.User.Commands.AddPhoto
{
    public class UserAddPhotoCommand : IRequest<UserPhotoVm>
    {
        public int UserId { get; set; }
        public IFormFile Photo { get; set; }
    }
}
