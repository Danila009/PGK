using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.User.Admin;

namespace PGK.Application.App.User.Admin.Commands.DeleteAdmin
{
    public class DeleteAdminCommandHandler : IRequestHandler<DeleteAdminCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public DeleteAdminCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
        {
            var admin = await _dbContext.AdminUsers.FindAsync(request.AdminId);

            if (admin == null)
            {
                throw new NotFoundException(nameof(AdminUser), request.AdminId);
            }

            _dbContext.AdminUsers.Remove(admin);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
