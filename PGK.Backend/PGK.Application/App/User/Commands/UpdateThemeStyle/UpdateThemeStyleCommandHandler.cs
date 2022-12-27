using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;

namespace PGK.Application.App.User.Commands.UpdateThemeStyle
{
    internal class UpdateThemeStyleCommandHandler
        : IRequestHandler<UpdateThemeStyleCommand, UpdateThemeStyleVm>
    {
        private readonly IPGKDbContext _dbContext;

        public UpdateThemeStyleCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<UpdateThemeStyleVm> Handle(UpdateThemeStyleCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(request.UserId);

            if(user == null)
            {
                throw new NotFoundException(nameof(Domain.User.User), request.UserId); ;
            }

            user.ThemeStyle = request.ThemeStyle;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateThemeStyleVm
            {
                ThemeStyle = user.ThemeStyle
            };
        }
    }
}
