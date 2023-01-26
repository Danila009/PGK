using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Common.Paged;
using PGK.Application.Interfaces;
using PGK.Domain.Journal;

namespace PGK.Application.App.Journal.Queries.GetJournalTopicList
{
    internal class GetJournalTopicListQueryHandler
        : IRequestHandler<GetJournalTopicListQuery, JournalTopicListVm>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetJournalTopicListQueryHandler(IPGKDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<JournalTopicListVm> Handle(GetJournalTopicListQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<JournalTopic> query = _dbContext.JournalTopics
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
                    .ThenInclude(u => u.Rows)
                        .ThenInclude(u => u.Student)
                            .ThenInclude(u => u.Group)
                                .ThenInclude(u => u.Speciality)
                .Include(u => u.JournalSubject)
                    .ThenInclude(u => u.Rows)
                        .ThenInclude(u => u.Student)
                            .ThenInclude(u => u.Group)
                                .ThenInclude(u => u.Department)
                .Include(u => u.JournalSubject)
                    .ThenInclude(u => u.Rows)
                        .ThenInclude(u => u.Student)
                            .ThenInclude(u => u.Group)
                                .ThenInclude(u => u.ClassroomTeacher)
                .Include(u => u.JournalSubject)
                    .ThenInclude(u => u.Rows)
                        .ThenInclude(u => u.Columns);

            if (request.JournalSubjectId != null)
            {
                query = query.Where(u => u.JournalSubject.Id == request.JournalSubjectId);
            }

            var journalTopic = query
                .OrderByDescending(u => u.Id)
                .ProjectTo<JournalTopicDto>(_mapper.ConfigurationProvider);

            var journalTopicPaged = await PagedList<JournalTopicDto>.ToPagedList(journalTopic,
                request.PageNumber, request.PageSize);

            return new JournalTopicListVm
            {
                Results = journalTopicPaged
            };
        }
    }
}
