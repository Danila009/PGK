using AutoMapper;
using MediatR;
using PGK.Application.App.User.DepartmentHead.Queries.GetDepartmentHeadList;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.User.DepartmentHead;

namespace PGK.Application.App.User.DepartmentHead.Queries.GetDepartmentHeadDetails
{
    internal class GetDepartmentHeadDetailsQueryHandler
        : IRequestHandler<GetDepartmentHeadDetailsQuery, DepartmentHeadDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetDepartmentHeadDetailsQueryHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<DepartmentHeadDto> Handle(GetDepartmentHeadDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var departmentHead = await _dbContext.DepartmentHeadUsers.FindAsync(request.Id);

            if (departmentHead == null)
            {
                throw new NotFoundException(nameof(DepartmentHeadUser), request.Id);
            }

            return _mapper.Map<DepartmentHeadDto>(departmentHead);
        }
    }
}
