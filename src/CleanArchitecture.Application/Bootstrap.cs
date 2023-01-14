using CleanArchitecture.Application.Common.Behaviours;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitecture.Application;

public static class Bootstrap
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        return services;
    }
}
