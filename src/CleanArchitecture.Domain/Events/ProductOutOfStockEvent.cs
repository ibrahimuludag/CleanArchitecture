namespace CleanArchitecture.Domain.Events;

public class ProductOutOfStockEvent : BaseEvent
{
    public ProductOutOfStockEvent(Product item)
    {
        Item = item;
    }

    public Product Item { get; }
}
