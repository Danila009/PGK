using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Common.Paged;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Vedomost.Queries.GetVedomostList
{
    internal class GetVedomostListQueryHandler
        : IRequestHandler<GetVedomostListQuery, VedomostListVm>
    {
        private readonly IMapper _mapper;
        private readonly IPGKDbContext _dbContext;

        public GetVedomostListQueryHandler(IMapper mapper, IPGKDbContext dbContext) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<VedomostListVm> Handle(GetVedomostListQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Domain.Vedomost.Vedomost> query = _dbContext.Vedomost
                .Include(u => u.Group)
                    .ThenInclude(u => u.ClassroomTeacher)
                .Include(u => u.Group)
                    .ThenInclude(u => u.Department)
                .Include(u => u.Group)
                    .ThenInclude(u => u.Speciality);

            if (request.OnlyDate != null)
            {
                query = query.Where(u => u.Date.Date == request.OnlyDate.Value.Date);
            }

            if (request.StartDate != null && request.OnlyDate == null)
            {
                query = query.Where(u => u.Date.Date > request.StartDate.Value.Date);
            }

            if (request.EndDate != null && request.OnlyDate == null)
            {
                query = query.Where(u => u.Date.Date < request.EndDate.Value.Date);
            }

            if(request.GroupIds != null && request.GroupIds.Count > 0)
            {
                query = query.Where(u => request.GroupIds.Contains(u.Group.Id));
            }

            var statements = query
                .ProjectTo<VedomostDto>(_mapper.ConfigurationProvider);

            var statementsPaged = await PagedList<VedomostDto>.ToPagedList(statements,
                request.PageNumber, request.PageSize);

            return new VedomostListVm { Results = statementsPaged };
        }
    }
}
