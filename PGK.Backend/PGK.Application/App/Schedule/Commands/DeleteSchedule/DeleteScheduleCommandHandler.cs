using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Schedule.Commands.DeleteSchedule
{
    internal class DeleteScheduleCommandHandler
        : IRequestHandler<DeleteScheduleCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public DeleteScheduleCommandHandler(IPGKDbContext dbContext)
            => _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteScheduleCommand request,
            CancellationToken cancellationToken)
        {
            var schedule = await _dbContext.Schedules.FindAsync(request.Id);

            if (schedule == null)
            {
                throw new NotFoundException(nameof(Domain.Schedules.Schedule), request.Id);
            }

            _dbContext.Schedules.Remove(schedule);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
