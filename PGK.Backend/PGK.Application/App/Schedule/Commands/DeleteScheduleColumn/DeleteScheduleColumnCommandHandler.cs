using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Schedule.Commands.DeleteScheduleColumn
{
    internal class DeleteScheduleColumnCommandHandler
        : IRequestHandler<DeleteScheduleColumnCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public DeleteScheduleColumnCommandHandler(IPGKDbContext dbContext)
            => _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteScheduleColumnCommand request,
            CancellationToken cancellationToken)
        {
            var column = await _dbContext.ScheduleColumns.FindAsync(request.Id);

            if (column == null)
            {
                throw new NotFoundException(nameof(Domain.Schedules.ScheduleColumn), request.Id);
            }

            _dbContext.ScheduleColumns.Remove(column);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
