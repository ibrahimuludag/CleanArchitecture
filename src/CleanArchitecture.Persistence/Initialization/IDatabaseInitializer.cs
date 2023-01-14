namespace CleanArchitecture.Persistence.Initialization;

public interface IDatabaseInitializer
{
    Task Initialize(CancellationToken cancellationToken);
}