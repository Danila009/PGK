using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using PGK.Application.Common.Paged;
using PGK.Application.Interfaces;
using PGK.Domain.User.Director;

namespace PGK.Application.App.User.Director.Queries.GetDirectorList
{
    internal class GetDirectorListQueryHandler
        : IRequestHandler<GetDirectorListQuery, DirectorListVm>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetDirectorListQueryHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<DirectorListVm> Handle(GetDirectorListQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<DirectorUser> query = _dbContext.DirectorUsers;

            if (!string.IsNullOrEmpty(request.Search))
            {
                var search = request.Search.ToLower().Trim();
                query = query.Where(u =>
                    u.FirstName.ToLower().Contains(search) ||
                    u.LastName.ToLower().Contains(search)
                );
            }

            if(request.Current != null)
            {
                query = query.Where(u => u.Current == request.Current);
            }

            var director = query
                 .OrderBy(u => u.FirstName)
                .OrderBy(u => u.LastName)
                .ProjectTo<DirectorDto>(_mapper.ConfigurationProvider);


            var directorPaged = await PagedList<DirectorDto>.ToPagedList(director,
                request.PageNumber, request.PageSize);

            return new DirectorListVm { Results = directorPaged };
        }
    }
}
