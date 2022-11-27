using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Common.Paged;
using PGK.Application.Interfaces;
using PGK.Domain.Raportichka;

namespace PGK.Application.App.Raportichka.Row.Queries.GetRaportichkaRowList
{
    public class GetRaportichkaRowListQueryHandler
        : IRequestHandler<GetRaportichkaRowListQuery, RaportichkaRowListVm>
    {
        private readonly IMapper _mapper;
        private readonly IPGKDbContext _dbContext;

        public GetRaportichkaRowListQueryHandler(IMapper mapper, IPGKDbContext dbContext) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<RaportichkaRowListVm> Handle(GetRaportichkaRowListQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<RaportichkaRow> query = _dbContext.RaportichkaRows
                .Include(u => u.Teacher)
                .Include(u => u.Student)
                .Include(u => u.Subject)
                .Include(u => u.Raportichka)
                .Where(u => u.Raportichka.Id == request.RaportichkaId);

            if (request.Confirmation != null)
            {
                query = query.Where(u => u.Confirmation == request.Confirmation);
            }

            if (request.SubjectIds != null && request.SubjectIds.Count > 0)
            {
                query = query.Where(u => request.SubjectIds.Contains(u.Subject.Id));
            }

            if (request.NumberLessons != null && request.NumberLessons.Count > 0)
            {
                query = query.Where(u => request.NumberLessons.Contains(u.NumberLesson));
            }

            if (request.TeacherIds != null && request.TeacherIds.Count > 0)
            {
                query = query.Where(u => request.TeacherIds.Contains(u.Teacher.Id));
            }

            if (request.StudentIds != null && request.StudentIds.Count > 0)
            {
                query = query.Where(u => request.StudentIds.Contains(u.Student.Id));
            }

            var rows = query
                .ProjectTo<RaportichkaRowDto>(_mapper.ConfigurationProvider);

            var rowsPaged = await PagedList<RaportichkaRowDto>.ToPagedList(rows,
                request.PageNumber, request.PageSize);

            return new RaportichkaRowListVm { Results = rowsPaged };
        }
    }
}
