using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Common.Paged;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Journal.Queries.GetJournalList
{
    public class GetJournalListQueryHandler
        : IRequestHandler<GetJournalListQuery, JournalListVm>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetJournalListQueryHandler(IPGKDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<JournalListVm> Handle(GetJournalListQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Domain.Journal.Journal> query = _dbContext.Journals
                .Include(u => u.Group)
                    .ThenInclude(u => u.Speciality)
                .Include(u => u.Group)
                    .ThenInclude(u => u.Department)
                .Include(u => u.Group)
                    .ThenInclude(u => u.ClassroomTeacher)
                .Include(u => u.Subjects)
                    .ThenInclude(u => u.Subject)
                .Include(u => u.Subjects)
                    .ThenInclude(u => u.Teacher)
                .Include(u => u.Subjects)
                    .ThenInclude(u => u.Topics)
                .Include(u => u.Subjects)
                    .ThenInclude(u => u.Rows)
                        .ThenInclude(u => u.Student)
                            .ThenInclude(u => u.Group)
                                .ThenInclude(u => u.Speciality)
                .Include(u => u.Subjects)
                    .ThenInclude(u => u.Rows)
                        .ThenInclude(u => u.Student)
                            .ThenInclude(u => u.Group)
                                .ThenInclude(u => u.Department)
                .Include(u => u.Subjects)
                    .ThenInclude(u => u.Rows)
                        .ThenInclude(u => u.Student)
                            .ThenInclude(u => u.Group)
                                .ThenInclude(u => u.ClassroomTeacher)
                .Include(u => u.Subjects)
                    .ThenInclude(u => u.Rows)
                        .ThenInclude(u => u.Columns);

            var journal = query
                .ProjectTo<JournalDto>(_mapper.ConfigurationProvider);

            var journalPaged = await PagedList<JournalDto>.ToPagedList(journal,
                request.PageNumber, request.PageSize);

            return new JournalListVm
            {
                Results = journalPaged
            };
        }
    }
}
