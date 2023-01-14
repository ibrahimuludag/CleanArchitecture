using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Domain.Common;
public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? LastModifiedDate { get; set; }
    public string? LastModifiedBy { get; set; }

    
    private readonly List<BaseEvent> _domainEvents = new();

    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
