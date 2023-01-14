namespace CleanArchitecture.Persistance.Initialization;

public interface IDatabaseInitializer
{
    Task Initialize(CancellationToken cancellationToken);
}