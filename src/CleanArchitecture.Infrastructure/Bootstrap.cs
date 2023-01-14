using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Models.Email;
using CleanArchitecture.Infrastructure.Authorization;
using CleanArchitecture.Infrastructure.Mail;

namespace CleanArchitecture.Infrastructure;

public static class Bootstrap
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddTransient<IEmailService, EmailService>();
        services.AddSwaggerDocument();
        
        // This is a fake authentication. Remove this on real scenario after implemention own authentication
        services.AddAuthentication(FakeAuthHandler.AuthenticationScheme)
              .AddScheme<FakeAuthHandlerOptions, FakeAuthHandler>(FakeAuthHandler.AuthenticationScheme, options => {
              });

        return services;
    }
}
