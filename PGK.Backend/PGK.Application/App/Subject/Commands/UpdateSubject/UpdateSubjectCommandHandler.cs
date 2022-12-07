using AutoMapper;
using MediatR;
using PGK.Application.App.Subject.Queries.GetSubjectList;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Subject.Commands.UpdateSubject
{
    internal class UpdateSubjectCommandHandler
        : IRequestHandler<UpdateSubjectCommand, SubjectDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateSubjectCommandHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<SubjectDto> Handle(UpdateSubjectCommand request,
            CancellationToken cancellationToken)
        {
            var subject = await _dbContext.Subjects.FindAsync(request.Id);

            if (subject == null)
            {
                throw new NotFoundException(nameof(Domain.Subject.Subject), request.Id);
            }

            subject.SubjectTitle = request.SubjectTitle;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<SubjectDto>(subject);
        }
    }
}
