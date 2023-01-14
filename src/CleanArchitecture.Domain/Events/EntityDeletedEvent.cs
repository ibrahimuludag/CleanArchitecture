namespace CleanArchitecture.Domain.Events;

public static class EntityDeletedEvent
{
    public static EntityDeletedEvent<TEntity> WithEntity<TEntity>(TEntity entity)
        where TEntity : BaseEntity
        => new(entity);
}

public class EntityDeletedEvent<TEntity> : BaseEvent
    where TEntity : BaseEntity
{
    internal EntityDeletedEvent(TEntity entity) => Entity = entity;

    public TEntity Entity { get; }
}
