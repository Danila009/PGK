using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Schedule.Commands.CreateSchedule
{
    public class CreateScheduleCommandHandler
        : IRequestHandler<CreateScheduleCommand, CreateScheduleVm>
    {
        private readonly IPGKDbContext _dbContext;
        
        public CreateScheduleCommandHandler(IPGKDbContext dbContext) => 
            _dbContext = dbContext;

        public async Task<CreateScheduleVm> Handle(CreateScheduleCommand request,
            CancellationToken cancellationToken)
        {
            var schedules = await _dbContext.Schedules.FirstOrDefaultAsync(u => u.Date.Date == request.Date.Date);

            if (schedules != null)
            {
                throw new Exception("Расписания за этот день уже создана.");
            }

            var schedule = new Domain.Schedules.Schedule
            {
                Date = request.Date
            };

            await _dbContext.Schedules.AddAsync(schedule, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateScheduleVm
            {
                ScheduleId = schedule.Id
            };
        }
    }
}
