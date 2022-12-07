using AutoMapper;
using MediatR;
using PGK.Application.App.Subject.Queries.GetSubjectList;
using PGK.Application.Interfaces;
using PGK.Application.Common.Exceptions;

namespace PGK.Application.App.Subject.Queries.GetSubjectDetails
{
    internal class GetSubjectDetailsQueryHandler
        : IRequestHandler<GetSubjectDetailsQuery, SubjectDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetSubjectDetailsQueryHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<SubjectDto> Handle(GetSubjectDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var subject = await _dbContext.Subjects.FindAsync(request.Id);

            if(subject == null)
            {
                throw new NotFoundException(nameof(Domain.Subject.Subject), request.Id);
            }

            return _mapper.Map<SubjectDto>(subject);
        }
    }
}
