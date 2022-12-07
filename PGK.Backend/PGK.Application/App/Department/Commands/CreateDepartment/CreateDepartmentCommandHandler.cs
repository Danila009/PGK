using AutoMapper;
using MediatR;
using PGK.Application.App.Department.Queries.GetDepartmentList;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.User.DepartmentHead;

namespace PGK.Application.App.Department.Commands.CreateDepartment
{
    internal class CreateDepartmentCommandHandler
        : IRequestHandler<CreateDepartmentCommand, DepartmentDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateDepartmentCommandHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<DepartmentDto> Handle(CreateDepartmentCommand request,
            CancellationToken cancellationToken)
        {
            var departmentHead = await _dbContext.DepartmentHeadUsers.FindAsync(request.DepartmentHeadId);

            if(departmentHead == null)
            {
                throw new NotFoundException(nameof(DepartmentHeadUser), request.DepartmentHeadId);
            }

            var department = new Domain.Department.Department
            {
                Name = request.Name,
                DepartmentHead = departmentHead
            };


            await _dbContext.Departments.AddAsync(department, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<DepartmentDto>(department);
        }
    }
}
