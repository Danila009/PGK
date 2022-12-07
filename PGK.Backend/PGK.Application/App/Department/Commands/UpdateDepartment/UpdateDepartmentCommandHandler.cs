using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.App.Department.Queries.GetDepartmentList;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.User.DepartmentHead;

namespace PGK.Application.App.Department.Commands.UpdateDepartment
{
    internal class UpdateDepartmentCommandHandler
        : IRequestHandler<UpdateDepartmentCommand, DepartmentDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateDepartmentCommandHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<DepartmentDto> Handle(UpdateDepartmentCommand request,
            CancellationToken cancellationToken)
        {
            var department = await _dbContext.Departments
                .Include(u => u.DepartmentHead)
                .FirstOrDefaultAsync(u => u.Id == request.DepartmentId);

            if(department == null)
            {
                throw new NotFoundException(nameof(Domain.Department.Department), request.DepartmentId);
            }

            var departmentHead = await _dbContext.DepartmentHeadUsers.FindAsync(request.DepartmentHeadId);

            if (departmentHead == null)
            {
                throw new NotFoundException(nameof(DepartmentHeadUser), request.DepartmentHeadId);
            }

            department.Name = request.Name;
            department.DepartmentHead = departmentHead;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<DepartmentDto>(department);
        }
    }
}
