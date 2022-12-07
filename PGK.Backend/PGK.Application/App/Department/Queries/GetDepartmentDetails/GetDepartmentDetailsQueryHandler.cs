using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.App.Department.Queries.GetDepartmentList;
using PGK.Application.Interfaces;
using PGK.Application.Common.Exceptions;

namespace PGK.Application.App.Department.Queries.GetDepartmentDetails
{
    internal class GetDepartmentDetailsQueryHandler
        : IRequestHandler<GetDepartmentDetailsQuery, DepartmentDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetDepartmentDetailsQueryHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);


        public async Task<DepartmentDto> Handle(GetDepartmentDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var department = await _dbContext.Departments
                .Include(u => u.DepartmentHead)
                .FirstOrDefaultAsync(u => u.Id == request.Id);

            if(department == null)
            {
                throw new NotFoundException(nameof(Domain.Department.Department), request.Id);
            }

            return _mapper.Map<DepartmentDto>(department);
        }
    }
}
