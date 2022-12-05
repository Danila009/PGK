using MediatR;
using PGK.Domain.User;

namespace PGK.Application.App.Vedomost.Commands.DeleteVedomost
{
    public class DeleteVedomostCommand : IRequest
    {
        public int VedomostId { get; set; }

        public int UserId { get; set; }
        public UserRole Role { get; set; }
    }
}
