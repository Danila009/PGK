using AutoMapper;
using MediatR;
using PGK.Application.App.User.Admin.Queries.GetAdminList;
using PGK.Application.Interfaces;
using PGK.Application.Common.Exceptions;
using PGK.Domain.User.Admin;

namespace PGK.Application.App.User.Admin.Queries.GetAdminDetails
{
    public class GetAdminDetailsQueryHandler
        : IRequestHandler<GetAdminDetailsQuery, AdminDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAdminDetailsQueryHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<AdminDto> Handle(GetAdminDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var admin = await _dbContext.AdminUsers.FindAsync(request.AdminId);

            if(admin == null)
            {
                throw new NotFoundException(nameof(AdminUser), request.AdminId);
            }

            return _mapper.Map<AdminDto>(admin);
        }
    }
}
