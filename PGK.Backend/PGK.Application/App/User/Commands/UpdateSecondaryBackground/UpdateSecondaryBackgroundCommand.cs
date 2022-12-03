using MediatR;
using PGK.Domain.User;

namespace PGK.Application.App.User.Commands.UpdateSecondaryBackground
{
    public class UpdateSecondaryBackgroundCommand
        : IRequest<UpdateSecondaryBackgroundVm>
    {
        public int UserId { get; set; }
        public SecondaryBackground SecondaryBackground { get; set; }
    }
}
