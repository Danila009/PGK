using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Schedule.Commands.DeleteScheduleRow
{
    internal class DeleteScheduleRowCommandHandler
        : IRequestHandler<DeleteScheduleRowCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public DeleteScheduleRowCommandHandler(IPGKDbContext dbContext)
            => _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteScheduleRowCommand request,
            CancellationToken cancellationToken)
        {
            var row = await _dbContext.ScheduleRows.FindAsync(request.Id);

            if (row == null)
            {
                throw new NotFoundException(nameof(Domain.Schedules.ScheduleRow), request.Id);
            }

            _dbContext.ScheduleRows.Remove(row);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
