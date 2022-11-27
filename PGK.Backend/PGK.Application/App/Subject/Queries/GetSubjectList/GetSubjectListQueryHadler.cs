using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Subject.Queries.GetSubjectList
{
    public class GetSubjectListQueryHadler
        : IRequestHandler<GetSubjectListQuery, SubjectListVm>
    {
        private readonly IMapper _mapper;
        private readonly IPGKDbContext _dbContext;

        public GetSubjectListQueryHadler(IMapper mapper, IPGKDbContext dbContext) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<SubjectListVm> Handle(GetSubjectListQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Domain.Subject.Subject> query = _dbContext.Subjects;

            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(u => u.SubjectTitle.ToLower()
                    .Contains(request.Search.ToLower()));
            }

            var subjects = await query
                .ProjectTo<SubjectDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new SubjectListVm { Subjects = subjects };
        }
    }
}
