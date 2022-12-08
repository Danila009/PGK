using AutoMapper;
using MediatR;
using PGK.Application.App.User.Student.Queries.GetStudentUserList;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.User.Student;

namespace PGK.Application.App.User.Student.Queries.GetStudentUserDetails
{
    internal class GetStudentUserDetailsQueryHandler
        : IRequestHandler<GetStudentUserDetailsQuery, StudentDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetStudentUserDetailsQueryHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<StudentDto> Handle(GetStudentUserDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var student = await _dbContext.StudentsUsers.FindAsync(request.Id);

            if (student == null)
            {
                throw new NotFoundException(nameof(StudentUser), request.Id);
            }

            return _mapper.Map<StudentDto>(student);
        }
    }
}
