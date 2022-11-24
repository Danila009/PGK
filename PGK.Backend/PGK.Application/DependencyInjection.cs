using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using PGK.Application.Security;
using PGK.Application.Common.Behaviors;

namespace Market.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection service, IConfiguration configuration)
        {

            service.AddScoped<IAuth>(provider => provider.GetService<Auth>());

            service.AddMediatR(Assembly.GetExecutingAssembly());

            service
                .AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });

            service.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            service.AddAuthSettings(configuration);

            return service;
        }
    }
}
