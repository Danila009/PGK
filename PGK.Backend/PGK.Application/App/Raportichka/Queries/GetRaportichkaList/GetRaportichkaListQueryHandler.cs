using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Common.Paged;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Raportichka.Queries.GetRaportichkaList
{
    public class GetRaportichkaListQueryHandler
        : IRequestHandler<GetRaportichkaListQuery, RaportichkaListVm>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetRaportichkaListQueryHandler(IPGKDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<RaportichkaListVm> Handle(GetRaportichkaListQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Domain.Raportichka.Raportichka> query = _dbContext.Raportichkas
                .Include(u => u.Group)
                    .ThenInclude(u => u.ClassroomTeacher)
                .Include(u => u.Group)
                    .ThenInclude(u => u.Department)
                .Include(u => u.Group)
                    .ThenInclude(u => u.Speciality)
                .Include(u => u.Rows)
                    .ThenInclude(u => u.Subject)
                .Include(u => u.Rows)
                    .ThenInclude(u => u.Teacher)
                .Include(u => u.Rows)
                    .ThenInclude(u => u.Student);


            if (request.Confirmation != null)
            {
                query = query.Where(u => u.Rows.Any(u => u.Confirmation == request.Confirmation));
            }

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

            if (request.SubjectIds != null && request.SubjectIds.Count > 0)
            {
                query = query.Where(u => u.Rows.Any(r => request.SubjectIds.Contains(r.Subject.Id)));
            }

            if (request.ClassroomTeacherIds != null && request.ClassroomTeacherIds.Count > 0)
            {
                query = query.Where(u => request.ClassroomTeacherIds.Contains(u.Group.ClassroomTeacher.Id));
            }

            if (request.NumberLessons != null && request.NumberLessons.Count > 0)
            {
                query = query.Where(u => u.Rows.Any(r => request.NumberLessons.Contains(r.NumberLesson)));
            }

            if (request.TeacherIds != null && request.TeacherIds.Count > 0)
            {
                query = query.Where(u => u.Rows.Any(r => request.TeacherIds.Contains(r.Teacher.Id)));
            }

            if (request.StudentIds != null && request.StudentIds.Count > 0)
            {
                query = query.Where(u => u.Rows.Any(r => request.StudentIds.Contains(r.Student.Id)));
            }

            var raportichka = query
                .ProjectTo<RaportichkaDto>(_mapper.ConfigurationProvider);

            var raportichkaPaged = await PagedList<RaportichkaDto>.ToPagedList(raportichka,
                request.PageNumber, request.PageSize);

            return new RaportichkaListVm
            {
                Results = raportichkaPaged
            };
        }
    }
}
