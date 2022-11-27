using MediatR;
using Microsoft.AspNetCore.Mvc;
using PGK.Domain.User;
using System.Security.Claims;

namespace PGK.WebApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public abstract class Controller : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        internal int UserId => !User.Identity.IsAuthenticated
            ? 0
            : int.Parse(User.FindFirst("user_id").Value);

        internal UserRole? UserRole => !User.Identity.IsAuthenticated
            ? null
            : (UserRole)Enum.Parse(typeof(UserRole),
                User.FindFirst(ClaimsIdentity.DefaultRoleClaimType).Value);


    }
}
