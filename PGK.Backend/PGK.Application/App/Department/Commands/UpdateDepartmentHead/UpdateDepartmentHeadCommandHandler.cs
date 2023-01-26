using MediatR;
using PGK.Application.Interfaces;
using PGK.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace PGK.Application.App.Department.Commands.UpdateDepartmentHead
{
    internal class UpdateDepartmentHeadCommandHandler
        : IRequestHandler<UpdateDepartmentHeadCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public UpdateDepartmentHeadCommandHandler(IPGKDbContext dbContext) =>
           _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateDepartmentHeadCommand request,
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
        
            if(departmentHead == null)
            {
                throw new NotFoundException(nameof(Domain.User.DepartmentHead), request.DepartmentHeadId);
            }

            department.DepartmentHead = departmentHead;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
