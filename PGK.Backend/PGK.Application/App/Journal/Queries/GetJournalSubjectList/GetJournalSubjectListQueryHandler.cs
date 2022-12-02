using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Common.Paged;
using PGK.Application.Interfaces;
using PGK.Domain.Journal;

namespace PGK.Application.App.Journal.Queries.GetJournalSubjectList
{
    internal class GetJournalSubjectListQueryHandler
        : IRequestHandler<GetJournalSubjectListQuery, JournalSubjectListVm>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetJournalSubjectListQueryHandler(IPGKDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<JournalSubjectListVm> Handle(GetJournalSubjectListQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<JournalSubject> query = _dbContext.JournalSubjects
                .Include(u => u.Subject)
                .Include(u => u.Teacher)
                .Include(u => u.Journal)
                    .ThenInclude(u => u.Group)
                        .ThenInclude(u => u.Speciality)
                .Include(u => u.Journal)
                    .ThenInclude(u => u.Group)
                        .ThenInclude(u => u.Department)
                .Include(u => u.Journal)
                    .ThenInclude(u => u.Group)
                        .ThenInclude(u => u.ClassroomTeacher)
                .Include(u => u.Topics)
                .Include(u => u.Rows)
                    .ThenInclude(u => u.Student)
                        .ThenInclude(u => u.Group)
                            .ThenInclude(u => u.Speciality)
                .Include(u => u.Rows)
                    .ThenInclude(u => u.Student)
                        .ThenInclude(u => u.Group)
                            .ThenInclude(u => u.Department)
                .Include(u => u.Rows)
                    .ThenInclude(u => u.Student)
                        .ThenInclude(u => u.Group)
                            .ThenInclude(u => u.ClassroomTeacher)
                .Include(u => u.Rows)
                    .ThenInclude(u => u.Columns);

            var journalSubject = query
                .ProjectTo<JournalSubjectDto>(_mapper.ConfigurationProvider);

            var journalPaged = await PagedList<JournalSubjectDto>.ToPagedList(journalSubject,
                request.PageNumber, request.PageSize);

            return new JournalSubjectListVm
            {
                Results = journalPaged
            };
        }
    }
}
