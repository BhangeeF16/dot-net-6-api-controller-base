using System.Collections.Concurrent;

namespace Domain.IServices.IUtilities;

public interface IHasDomainEventEntity
{
    IProducerConsumerCollection<IDomainEvent> DomainEvents { get; }
}

public interface IDomainEvent
{
    public bool IsPublished { get; }
    public DateTimeOffset DateOccurred { get; }
}
public interface IDomainEventDispatcher
{
    Task Dispatch(IDomainEvent devent);
}
