using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;

namespace PGK.Application.App.User.Commands.UpdateDrarkMode
{
    internal class UpdateDrarkModeCommandHandler
        : IRequestHandler<UpdateDrarkModeCommand, UpdateDrarkModeVm>
    {
        private readonly IPGKDbContext _dbContext;

        public UpdateDrarkModeCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<UpdateDrarkModeVm> Handle(UpdateDrarkModeCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(request.UserId);

            if(user == null)
            {
                throw new NotFoundException(nameof(Domain.User.User), request.UserId);
            }

            user.DrarkMode = !user.DrarkMode;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateDrarkModeVm
            {
                DrarkMode = user.DrarkMode
            };
        }
    }
}
