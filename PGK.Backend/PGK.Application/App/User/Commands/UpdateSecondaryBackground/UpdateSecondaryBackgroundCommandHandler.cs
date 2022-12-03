using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;

namespace PGK.Application.App.User.Commands.UpdateSecondaryBackground
{
    internal class UpdateSecondaryBackgroundCommandHandler
        : IRequestHandler<UpdateSecondaryBackgroundCommand, UpdateSecondaryBackgroundVm>
    {
        private readonly IPGKDbContext _dbContext;

        public UpdateSecondaryBackgroundCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<UpdateSecondaryBackgroundVm> Handle(
            UpdateSecondaryBackgroundCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(request.UserId);

            if(user == null)
            {
                throw new NotFoundException(nameof(Domain.User.User), request.UserId); ;
            }

            user.SecondaryBackground = request.SecondaryBackground;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateSecondaryBackgroundVm
            {
                SecondaryBackground = user.SecondaryBackground
            };
        }
    }
}
