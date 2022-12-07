using AutoMapper;
using MediatR;
using PGK.Application.App.Speciality.Queries.GetSpecialityList;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Speciality.Commands.CreateSpeciality
{
    internal class CreateSpecialityCommandHandler
        : IRequestHandler<CreateSpecialityCommand, SpecialityDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateSpecialityCommandHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<SpecialityDto> Handle(CreateSpecialityCommand request,
            CancellationToken cancellationToken)
        {
            var department = await _dbContext.Departments.FindAsync(request.DepartmentId);

            if(department == null)
            {
                throw new NotFoundException(nameof(Domain.Department.Department), request.DepartmentId);
            }

            var speciality = new Domain.Speciality.Speciality
            {
                Number = request.Number,
                Name = request.Name,
                NameAbbreviation = request.NameAbbreviation,
                Qualification = request.Qualification,
                Department = department
            };

            await _dbContext.Departments.AddAsync(department, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<SpecialityDto>(speciality);
        }
    }
}
