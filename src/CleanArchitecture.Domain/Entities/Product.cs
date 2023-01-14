using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Domain.Entities;

public class Product : BaseEntity
{
    private decimal? _stock = null;

    public string Title { get; set; } = default!;

    public string Sku { get; set; } = default!;
    
    public Category? Category { get; set; }
    
    public decimal? Price { get; set; }
    
    public decimal? Stock { 
        get {
            return _stock;
        } 
        set {
            if (value < 0)
            {
                throw new InvalidStockException(value.Value.ToString());
            }

            if (value == 0)
            {
                AddDomainEvent(new ProductOutOfStockEvent(this));
            }

            _stock = value;
        }
    }
    
    public Status Status { get; set; }
    
    public Weight? Weight { get; set; }
}
