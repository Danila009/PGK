using Market.Application;
using Market.WebApi.Swagger;
using Microsoft.OpenApi.Models;
using PGK.Application.Common.Mappings;
using PGK.Application.Interfaces;
using PGK.Persistence;
using PGK.WebApi.Middleware;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

RegisterServices(builder.Services);

var app = builder.Build();

Configure(app);

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    try
    {
        var context = serviceProvider.GetRequiredService<PGKDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
}

app.Run();


void RegisterServices(IServiceCollection services)
{
    services.AddAutoMapper(config =>
    {
        config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
        config.AddProfile(new AssemblyMappingProfile(typeof(IPGKDbContext).Assembly));
    });

    services.AddApplication(builder.Configuration);
    services.AddPersistence();

    services.AddControllers()
               .AddJsonOptions(
                   opt => opt.JsonSerializerOptions
                   .Converters.Add(new JsonStringEnumConverter()));

    services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
        });
    });

    services.AddSwaggerGen(config =>
    {
        config.AddAuthSwagger();

        config.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1.1",
            Title = "PGK API",
            Contact = new OpenApiContact
            {
                Name = "GitHub",
                Url = new Uri("https://github.com/Danila009/PGK")
            }
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        config.IncludeXmlComments(xmlPath);
    });
}

void Configure(WebApplication app)
{
    var basePath = "pgk63";

    app.UsePathBase("/" + basePath);
    {
        app.UseDeveloperExceptionPage();

        app.UseSwagger(c =>
        {
            c.RouteTemplate = "swagger/{documentName}/swagger.json";
            c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
            {
                swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer {
                    Url = $"https://{httpReq.Host.Value}/{basePath}" } };
            });
        });
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"/{basePath}/swagger/v1/swagger.json", "PGK API v1");
        });
    }

    app.UseCustomExceptionHandler();

    app.UseRouting();
    app.UseCors("AllowAll");

    app.UseAuthentication();
    app.UseAuthorization();

    var webSocketOptions = new WebSocketOptions
    {
        KeepAliveInterval = TimeSpan.FromMinutes(2)
    };

    app.UseWebSockets(webSocketOptions);

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}