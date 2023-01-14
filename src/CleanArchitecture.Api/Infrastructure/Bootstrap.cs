using CleanArchitecture.Application.Contracts.Authorization;
using CleanArchitecture.Infrastructure.Services;
using CleanArchitecture.Persistance.Initialization;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Mapster;

namespace CleanArchitecture.Api.Infrastructure;

public static class Bootstrap
{
    public static async Task MigrateDatabase(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
            await dbInitializer.Initialize(CancellationToken.None);
        }
    }

    public static async Task InitializeDatabase(this WebApplication app, bool isDevelopment)
    {
        using (var scope = app.Services.CreateScope())
        {
            var seeder = scope.ServiceProvider.GetRequiredService<IApplicationDbSeeder>();
            await seeder.SeedDatabase(isDevelopment);
        }
    }

    public static IServiceCollection AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

        builder.Services.AddApiVersioning(options => {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
        });

        builder.Services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddMemoryCache();

        builder.Services.AddFluentValidationAutoValidation(conf =>
        {
            conf.DisableDataAnnotationsValidation = true;
        })
        .AddValidatorsFromAssemblyContaining<Api.Locator>()
        .AddValidatorsFromAssemblyContaining<Application.Locator>();

        builder.Services.Configure<ApiBehaviorOptions>(options =>
                   options.SuppressModelStateInvalidFilter = true);

        ConfigureMapper();


        return builder.Services;
    }

    private static void ConfigureMapper() {
        TypeAdapterConfig.GlobalSettings.Default.MapToConstructor(true);
    }
}
