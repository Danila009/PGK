using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            var student = await _dbContext.StudentsUsers
                .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (student == null)
            {
                throw new NotFoundException(nameof(StudentUser), request.Id);
            }

            return student;
        }
    }
}
