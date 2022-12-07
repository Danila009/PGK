using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Common.Paged;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Department.Queries.GetDepartmentList
{
    internal class GetDepartmentListQueryHandler
        : IRequestHandler<GetDepartmentListQuery, DepartmentListVm>
    {
        private readonly IMapper _mapper;
        private readonly IPGKDbContext _dbContext;

        public GetDepartmentListQueryHandler(IMapper mapper, IPGKDbContext dbContext) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<DepartmentListVm> Handle(GetDepartmentListQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Domain.Department.Department> query = _dbContext.Departments
                .Include(u => u.DepartmentHead);

            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(u => u.Name.ToLower().Contains(request.Search.ToLower()));
            }

            var departments = query
                .ProjectTo<DepartmentDto>(_mapper.ConfigurationProvider);

            var departmentPaged = await PagedList<DepartmentDto>.ToPagedList(departments,
                request.PageNumber, request.PageSize);

            return new DepartmentListVm { Results = departmentPaged };
        }
    }
}
