using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Common.Paged;
using PGK.Application.Interfaces;
using PGK.Domain.Journal;

namespace PGK.Application.App.Journal.Queries.GetJournalSubjectRowList
{
    internal class GetJournalSubjectRowListQueryHandler
        : IRequestHandler<GetJournalSubjectRowListQuery, JournalSubjectRowListVm>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetJournalSubjectRowListQueryHandler(IPGKDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<JournalSubjectRowListVm> Handle(GetJournalSubjectRowListQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<JournalSubjectRow> query = _dbContext.JournalSubjectRows
                .Include(u => u.Student)
                    .ThenInclude(u => u.Group)
                        .ThenInclude(u => u.Speciality)
                .Include(u => u.Student)
                    .ThenInclude(u => u.Group)
                        .ThenInclude(u => u.Department)
                .Include(u => u.Student)
                    .ThenInclude(u => u.Group)
                        .ThenInclude(u => u.ClassroomTeacher)
                .Include(u => u.JournalSubject)
                    .ThenInclude(u => u.Subject)
                .Include(u => u.JournalSubject)
                    .ThenInclude(u => u.Teacher)
                .Include(u => u.JournalSubject)
                    .ThenInclude(u => u.Journal)
                        .ThenInclude(u => u.Group)
                            .ThenInclude(u => u.Speciality)
                .Include(u => u.JournalSubject)
                    .ThenInclude(u => u.Journal)
                        .ThenInclude(u => u.Group)
                            .ThenInclude(u => u.Department)
                .Include(u => u.JournalSubject)
                    .ThenInclude(u => u.Journal)
                        .ThenInclude(u => u.Group)
                            .ThenInclude(u => u.ClassroomTeacher)
                .Include(u => u.JournalSubject)
                    .ThenInclude(u => u.Topics)
                .Include(u => u.Columns);

            var journalSubjectRow = query
                .ProjectTo<JournalSubjectRowDto>(_mapper.ConfigurationProvider);

            var journalRowPaged = await PagedList<JournalSubjectRowDto>.ToPagedList(journalSubjectRow,
                request.PageNumber, request.PageSize);

            return new JournalSubjectRowListVm
            {
                Results = journalRowPaged
            };
        }
    }
}
