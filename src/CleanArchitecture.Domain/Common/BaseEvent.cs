using MediatR;

namespace CleanArchitecture.Domain.Common;

public abstract class BaseEvent : INotification
{
    public Dictionary<string, string> MetaData { get; init; } = new();
}
