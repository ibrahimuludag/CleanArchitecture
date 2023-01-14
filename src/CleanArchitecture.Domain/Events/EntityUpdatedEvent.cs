namespace CleanArchitecture.Domain.Events;

public static class EntityUpdatedEvent
{
    public static EntityUpdatedEvent<TEntity> WithEntity<TEntity>(TEntity entity)
        where TEntity : BaseEntity
        => new(entity);
}

public class EntityUpdatedEvent<TEntity> : BaseEvent
    where TEntity : BaseEntity
{
    internal EntityUpdatedEvent(TEntity entity) => Entity = entity;

    public TEntity Entity { get; }
}