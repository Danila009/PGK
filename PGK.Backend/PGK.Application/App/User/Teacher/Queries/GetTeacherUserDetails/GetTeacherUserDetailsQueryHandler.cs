using AutoMapper;
using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.User.Teacher;

namespace PGK.Application.App.User.Teacher.Queries.GetTeacherUserDetails
{
    internal class GetTeacherUserDetailsQueryHandler
        : IRequestHandler<GetTeacherUserDetailsQuery, TeacherUserDetails>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetTeacherUserDetailsQueryHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<TeacherUserDetails> Handle(GetTeacherUserDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var teacher = await _dbContext.TeacherUsers.FindAsync(request.Id);

            if (teacher == null)
            {
                throw new NotFoundException(nameof(TeacherUser), request.Id);
            }

            return _mapper.Map<TeacherUserDetails>(teacher);
        }
    }
}
