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

            if(request.Courses != null && request.Courses.Count > 0)
            {
                query = query.Where(u => request.Courses.Contains(u.Course));
            }

            if (request.Semesters != null && request.Semesters.Count > 0)
            {
                query = query.Where(u => request.Semesters.Contains(u.Semester));
            }

            if (request.GroupIds != null && request.GroupIds.Count > 0)
            {
                query = query.Where(u => request.GroupIds.Contains(u.Group.Id));
            }

            if (request.SpecialityIds != null && request.SpecialityIds.Count > 0)
            {
                query = query.Where(u => request.SpecialityIds.Contains(u.Group.Speciality.Id));
            }

            if (request.DepartmentIds != null && request.DepartmentIds.Count > 0)
            {
                query = query.Where(u => request.DepartmentIds.Contains(u.Group.Department.Id));
            }

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
