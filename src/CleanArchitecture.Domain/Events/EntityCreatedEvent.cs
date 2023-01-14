namespace CleanArchitecture.Domain.Events;

public static class EntityCreatedEvent
{
    public static EntityCreatedEvent<TEntity> WithEntity<TEntity>(TEntity entity)
        where TEntity : BaseEntity
        => new(entity);
}

public class EntityCreatedEvent<TEntity> : BaseEvent
    where TEntity : BaseEntity
{
    internal EntityCreatedEvent(TEntity entity) => Entity = entity;

    public TEntity Entity { get; }
}