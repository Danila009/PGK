using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.Schedules;

namespace PGK.Application.App.Schedule.Commands.CreateScheduleDepartment
{
    public class CreateScheduleDepartmentCommandHandler
        : IRequestHandler<CreateScheduleDepartmentCommand, CreateScheduleDepartmentVm>
    {
        private readonly IPGKDbContext _dbContext;

        public CreateScheduleDepartmentCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<CreateScheduleDepartmentVm> Handle(CreateScheduleDepartmentCommand request,
            CancellationToken cancellationToken)
        {
            var department = await _dbContext.Departments.FindAsync(request.DepartmentId);

            if (department == null)
            {
                throw new NotFoundException(nameof(Domain.Department.Department), request.DepartmentId);
            }

            var schedule = await _dbContext.Schedules.FindAsync(request.ScheduleId);

            if (schedule == null)
            {
                throw new NotFoundException(nameof(Domain.Schedules.Schedule), request.ScheduleId);
            }

            var scheduleDepartment = new ScheduleDepartment
            {
                Department = department,
                Schedule = schedule
            };

            await _dbContext.ScheduleDepartments.AddAsync(scheduleDepartment, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateScheduleDepartmentVm
            {
                ScheduleDepartmentId = scheduleDepartment.Id
            };
        }
    }
}
