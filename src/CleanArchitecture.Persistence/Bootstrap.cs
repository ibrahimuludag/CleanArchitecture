﻿using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Persistence.Context;
using CleanArchitecture.Persistence.Initialization;
using CleanArchitecture.Persistence.Initialization.Seed;
using CleanArchitecture.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Persistence;

public static class Bootstrap
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("ApplicationDbContextTest");
            });
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CleanArchitectureConnectionString"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        }

        services.Scan(scan => scan
        .FromAssembliesOf(typeof(IAsyncRepository<>))
        .AddClasses(classes => classes.AssignableTo(typeof(IAsyncRepository<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan
        .FromAssembliesOf(typeof(BaseRepository<>))
        .AddClasses(classes => classes.AssignableTo(typeof(BaseRepository<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.AddScoped<IDatabaseInitializer, ApplicationDbInitializer>();
        services.AddScoped<IApplicationDbSeeder, ApplicationDbSeeder>();

        services.Scan(scan => scan
        .FromAssembliesOf(typeof(ICustomSeeder))
        .AddClasses(classes => classes.AssignableTo(typeof(ICustomSeeder)))
        .AsImplementedInterfaces()
            .WithTransientLifetime());

        services.AddScoped<ICustomSeedRunner, CustomSeedRunner>();

        return services;
    }
}
