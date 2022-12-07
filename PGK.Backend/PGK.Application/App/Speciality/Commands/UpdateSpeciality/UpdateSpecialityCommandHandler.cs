using AutoMapper;
using MediatR;
using PGK.Application.App.Speciality.Queries.GetSpecialityList;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Speciality.Commands.UpdateSpeciality
{
    internal class UpdateSpecialityCommandHandler
        : IRequestHandler<UpdateSpecialityCommand, SpecialityDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateSpecialityCommandHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<SpecialityDto> Handle(UpdateSpecialityCommand request,
            CancellationToken cancellationToken)
        {
            var speciality = await _dbContext.Specialties.FindAsync(request.Id);

            if (speciality == null)
            {
                throw new NotFoundException(nameof(Domain.Speciality.Speciality), request.Id);
            }

            var department = await _dbContext.Departments.FindAsync(request.DepartmentId);

            if (department == null)
            {
                throw new NotFoundException(nameof(Domain.Department.Department), request.DepartmentId);
            }

            speciality.Number = request.Number;
            speciality.Name = request.Name;
            speciality.NameAbbreviation = request.NameAbbreviation;
            speciality.Qualification = request.Qualification;
            speciality.Department = department;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<SpecialityDto>(speciality);
        }
    }
}
