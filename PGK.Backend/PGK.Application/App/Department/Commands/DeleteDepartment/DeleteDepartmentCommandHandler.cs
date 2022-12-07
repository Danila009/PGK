using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Department.Commands.DeleteDepartment
{
    internal class DeleteDepartmentCommandHandler
        : IRequestHandler<DeleteDepartmentCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public DeleteDepartmentCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteDepartmentCommand request,
            CancellationToken cancellationToken)
        {
            var department = await _dbContext.Departments.FindAsync(request.Id);

            if (department == null)
            {
                throw new NotFoundException(nameof(Domain.Department.Department), request.Id);
            }

            _dbContext.Departments.Remove(department);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
