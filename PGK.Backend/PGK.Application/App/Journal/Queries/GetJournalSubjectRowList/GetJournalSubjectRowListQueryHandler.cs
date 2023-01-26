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

            if(request.JournalSubjectId != null)
            {
                query = query.Where(u => u.JournalSubject.Id == request.JournalSubjectId);
            }

            if(request.StudentIds != null && request.StudentIds.Count > 0)
            {
                query = query.Where(u => request.StudentIds.Contains(u.Student.Id));
            }

            if(request.Evaluation != null)
            {
                query = query.Where(u => u.Columns.Any(u => u.Evaluation == request.Evaluation));
            }

            var journalSubjectRow = query
                .OrderByDescending(u => u.Id)
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
