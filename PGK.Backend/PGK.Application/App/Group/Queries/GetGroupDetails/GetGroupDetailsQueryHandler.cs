using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Group.Queries.GetGroupDetails
{
    public class GetGroupDetailsQueryHandler
        : IRequestHandler<GetGroupDetailsQuery, GroupDetails>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetGroupDetailsQueryHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<GroupDetails> Handle(GetGroupDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var group = await _dbContext.Groups
                .Include(u => u.ClassroomTeacher)
                .Include(u => u.Speciality)
                .Include(u => u.Department)
                .Include(u => u.DeputyHeadma)
                .Include(u => u.Headman)
                .ProjectTo<GroupDetails>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(u => u.Id == request.GroupId, cancellationToken);

            if (group == null)
            {
                throw new NotFoundException(nameof(Domain.Group.Group), request.GroupId);
            }

            return group;
        }
    }
}
