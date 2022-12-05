using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using PGK.Application.Security;
using PGK.Application.Common.Behaviors;
using PGK.Application.Repository.ImageRepository;
using PGK.Application.Services.EmailService;
using PGK.Application.Repository.FileRepository;

namespace Market.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection service, IConfiguration configuration)
        {
            service.AddSingleton<IAuth, Auth>();
            service.AddSingleton<IImageRepository, ImageRepository>();
            service.AddSingleton<IFileRepository, FileRepository>();
            service.AddSingleton<IEmailService, EmailService>();

            service.AddMediatR(Assembly.GetExecutingAssembly());

            service
                .AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });

            service.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            service.AddAuthSettings(configuration);

            return service;
        }
    }
}
