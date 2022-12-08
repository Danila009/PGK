using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Schedule.Commands.DeleteScheduleDepartment
{
    internal class DeleteScheduleDepartmentCommandHandler
        : IRequestHandler<DeleteScheduleDepartmentCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public DeleteScheduleDepartmentCommandHandler(IPGKDbContext dbContext)
            => _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteScheduleDepartmentCommand request,
            CancellationToken cancellationToken)
        {
            var scheduleDepartment = await _dbContext.ScheduleDepartments.FindAsync(request.Id);

            if (scheduleDepartment == null)
            {
                throw new NotFoundException(nameof(Domain.Schedules.ScheduleDepartment), request.Id);
            }

            _dbContext.ScheduleDepartments.Remove(scheduleDepartment);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
