using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.User.DepartmentHead;

namespace PGK.Application.App.User.DepartmentHead.Commands.DeleteDepartmentHead
{
    internal class DeleteDepartmentHeadCommandHandler
        : IRequestHandler<DeleteDepartmentHeadCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public DeleteDepartmentHeadCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteDepartmentHeadCommand request,
            CancellationToken cancellationToken)
        {
            var departmentHead = await _dbContext.DepartmentHeadUsers.FindAsync(request.Id);

            if (departmentHead == null)
            {
                throw new NotFoundException(nameof(DepartmentHeadUser), request.Id);
            }

            _dbContext.DepartmentHeadUsers.Remove(departmentHead);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
