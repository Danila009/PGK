using MediatR;
using Microsoft.AspNetCore.Http;
using PGK.Domain.User;

namespace PGK.Application.App.Vedomost.Commands.CreateVedomost
{
    public class CreateVedomostCommand : IRequest<CreateVedomostVm>
    {
        public IFormFile File { get; set; }

        public DateTime Date { get; set; }

        public UserRole Role { get; set; }
        public int UserId { get; set; }

        public int? GroupId { get; set; }
    }
}
