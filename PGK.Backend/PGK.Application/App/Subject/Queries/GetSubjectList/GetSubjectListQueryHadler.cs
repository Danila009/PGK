using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Common.Paged;
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
            IQueryable<Domain.Subject.Subject> query = _dbContext.Subjects
                .Include(u => u.Teachers);

            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(u => u.SubjectTitle.ToLower()
                    .Contains(request.Search.ToLower()));
            }

            if(request.TeacherIds != null && request.TeacherIds.Count > 0)
            {
                query = query.Where(u => u.Teachers.Any(u => request.TeacherIds.Contains(u.Id)));
            }

            var subjects = query
                .OrderBy(u => u.SubjectTitle)
                .ProjectTo<SubjectDto>(_mapper.ConfigurationProvider);

            var subjectsPaged = await PagedList<SubjectDto>.ToPagedList(subjects,
                request.PageNumber, request.PageSize);

            return new SubjectListVm { Results = subjectsPaged };
        }
    }
}
