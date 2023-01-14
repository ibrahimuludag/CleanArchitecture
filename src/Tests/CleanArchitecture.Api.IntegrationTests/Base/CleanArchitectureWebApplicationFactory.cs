///https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-7.0
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CleanArchitecture.Api.IntegrationTests.Base;

public class CleanArchitectureWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("it");
    }

    public HttpClient GetClient()
    {
        return CreateClient();
    }
}