using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.App.Schedule.GetScheduleList.Queries;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Schedule.Commands.UpdateSchedule
{
    internal class UpdateScheduleCommandHandler
        : IRequestHandler<UpdateScheduleCommand, ScheduleDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateScheduleCommandHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<ScheduleDto> Handle(UpdateScheduleCommand request,
            CancellationToken cancellationToken)
        {
            var schedules = await _dbContext.Schedules.FirstOrDefaultAsync(u => u.Date.Date == request.Date.Date);

            if (schedules != null)
            {
                throw new Exception("Расписания за этот день уже создана.");
            }

            var schedule = await _dbContext.Schedules.FindAsync(request.Id);

            if (schedule == null)
            {
                throw new NotFoundException(nameof(Domain.Schedules.Schedule), request.Id);
            }

            schedule.Date = request.Date;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ScheduleDto>(schedule);
        }
    }
}
