using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Persistance.Initialization;

public interface IApplicationDbSeeder
{
    Task SeedDatabase(bool isDevelopment);
}
public class ApplicationDbSeeder : IApplicationDbSeeder
{
    private readonly ICustomSeedRunner _seederRunner;
    private readonly ILogger<ApplicationDbSeeder> _logger;

    public ApplicationDbSeeder(ICustomSeedRunner seederRunner, ILogger<ApplicationDbSeeder> logger)
    {
        _seederRunner = seederRunner;
        _logger = logger;
    }

    public async Task SeedDatabase(bool isDevelopment)
    {
        _logger.LogInformation("Running seed runners");
        await _seederRunner.RunSeeders(isDevelopment);
    }
}