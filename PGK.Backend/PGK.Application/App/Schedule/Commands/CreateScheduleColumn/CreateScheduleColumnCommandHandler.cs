using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.Schedules;

namespace PGK.Application.App.Schedule.Commands.CreateScheduleColumn
{
    internal class CreateScheduleColumnCommandHandler
        : IRequestHandler<CreateScheduleColumnCommand, CreateScheduleColumnVm>
    {
        private readonly IPGKDbContext _dbContext;

        public CreateScheduleColumnCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<CreateScheduleColumnVm> Handle(CreateScheduleColumnCommand request,
            CancellationToken cancellationToken)
        {
            var group = await _dbContext.Groups.FindAsync(request.GroupId);
            
            if(group == null)
            {
                throw new NotFoundException(nameof(Domain.Group.Group), request.GroupId);
            }

            var scheduleDepartment = await _dbContext.ScheduleDepartments
                .FindAsync(request.ScheduleDepartmentId);

            if(scheduleDepartment == null)
            {
                throw new NotFoundException(nameof(ScheduleDepartment), request.ScheduleDepartmentId);
            }

            var column = new ScheduleColumn
            {
                Time = request.Time,
                Group = group,
                ScheduleDepartment = scheduleDepartment
            };

            return new CreateScheduleColumnVm
            {
                ScheduleColumnId = column.Id
            };
        }
    }
}
