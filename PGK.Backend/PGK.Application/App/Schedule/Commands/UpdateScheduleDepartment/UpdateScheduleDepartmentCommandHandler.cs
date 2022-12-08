using AutoMapper;
using MediatR;
using PGK.Application.App.Schedule.Queries.GetScheduleDepartmentList;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.Schedules;

namespace PGK.Application.App.Schedule.Commands.UpdateScheduleDepartment
{
    internal class UpdateScheduleDepartmentCommandHandler
        : IRequestHandler<UpdateScheduleDepartmentCommand, ScheduleDepartmentDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateScheduleDepartmentCommandHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<ScheduleDepartmentDto> Handle(UpdateScheduleDepartmentCommand request,
            CancellationToken cancellationToken)
        {
            var scheduleDepartment = await _dbContext.ScheduleDepartments.FindAsync(request.Id);

            if (scheduleDepartment == null)
            {
                throw new NotFoundException(nameof(ScheduleDepartment), request.Id);

            }

            var department = await _dbContext.Departments.FindAsync(request.DepartmentId);

            if (department == null)
            {
                throw new NotFoundException(nameof(Domain.Department.Department), request.DepartmentId);
            }

            scheduleDepartment.Text = request.Text;
            scheduleDepartment.Department = department;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ScheduleDepartmentDto>(scheduleDepartment);
        }
    }
}
