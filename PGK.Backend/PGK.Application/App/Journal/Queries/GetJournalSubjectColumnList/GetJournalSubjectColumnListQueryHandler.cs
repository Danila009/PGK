using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Common.Paged;
using PGK.Application.Interfaces;
using PGK.Domain.Journal;

namespace PGK.Application.App.Journal.Queries.GetJournalSubjectColumnList
{
    internal class GetJournalSubjectColumnListQueryHandler
        : IRequestHandler<GetJournalSubjectColumnListQuery, JournalSubjectColumnListVm>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetJournalSubjectColumnListQueryHandler(IPGKDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<JournalSubjectColumnListVm> Handle(GetJournalSubjectColumnListQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<JournalSubjectColumn> query = _dbContext.JournalSubjectColumns
                .Include(u => u.Row)
                    .ThenInclude(u => u.Student)
                        .ThenInclude(u => u.Group)
                            .ThenInclude(u => u.Speciality)
                .Include(u => u.Row)
                    .ThenInclude(u => u.Student)
                        .ThenInclude(u => u.Group)
                            .ThenInclude(u => u.Department)
                .Include(u => u.Row)
                    .ThenInclude(u => u.Student)
                        .ThenInclude(u => u.Group)
                            .ThenInclude(u => u.ClassroomTeacher)
                .Include(u => u.Row)
                    .ThenInclude(u => u.JournalSubject)
                        .ThenInclude(u => u.Subject)
                .Include(u => u.Row)
                    .ThenInclude(u => u.JournalSubject)
                        .ThenInclude(u => u.Teacher)
                .Include(u => u.Row)
                    .ThenInclude(u => u.JournalSubject)
                        .ThenInclude(u => u.Journal)
                            .ThenInclude(u => u.Group)
                                .ThenInclude(u => u.Speciality)
                .Include(u => u.Row)
                    .ThenInclude(u => u.JournalSubject)
                        .ThenInclude(u => u.Journal)
                            .ThenInclude(u => u.Group)
                                .ThenInclude(u => u.Department)
                .Include(u => u.Row)
                    .ThenInclude(u => u.JournalSubject)
                        .ThenInclude(u => u.Journal)
                            .ThenInclude(u => u.Group)
                                .ThenInclude(u => u.ClassroomTeacher)
                .Include(u => u.Row)
                    .ThenInclude(u => u.JournalSubject)
                        .ThenInclude(u => u.Topics);

            if(request.JournalRowId != null)
            {
                query = query.Where(u => u.Row.Id == request.JournalRowId);
            }

            if (request.StudentIds != null && request.StudentIds.Count > 0)
            {
                query = query.Where(u => request.StudentIds.Contains(u.Row.Student.Id));
            }

            if (request.Evaluation != null)
            {
                query = query.Where(u => u.Evaluation == request.Evaluation);
            }

            var journalSubjectColumn = query
                .OrderByDescending(u => u.Id)
                .ProjectTo<JournalSubjectColumnDto>(_mapper.ConfigurationProvider);

            var journalColumnPaged = await PagedList<JournalSubjectColumnDto>.ToPagedList(journalSubjectColumn,
                request.PageNumber, request.PageSize);

            return new JournalSubjectColumnListVm
            {
                Results = journalColumnPaged
            };
        }
    }
}
