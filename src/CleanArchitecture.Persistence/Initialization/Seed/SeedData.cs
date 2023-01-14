using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Persistence.Initialization.Seed;

public static class SeedData
{
    public static List<Product> Products => new List<Product> {
        new Product
            {
                Id = Guid.Parse("4f1394f4-4287-460c-9669-6ab8b5fb7bda"),
                Title = "Harry Potter",
                Sku = "SKU-B-100-1"
            },
        new Product
            {
                Id = Guid.Parse("57dddcde-8284-4c91-b3f2-7a43122be1f0"),
                Title = "1984",
                Sku = "SKU-B-100-2"
            },
         new Product
            {
                Id = Guid.Parse("65878a0e-78ef-42c4-8fb6-0e09b256d472"),
                Title = "Sugar",
                Sku = "SKU-B-200-1",
                Weight = new Weight(Domain.Enums.MassUnits.Kilogram, 1)
            }
    };

    public static List<Category> Categories => new List<Category> {
        new Category { Id = Guid.Parse("88941081-2cab-4a32-bd33-e84dc12c6bd9"), Name = "Book" },
        new Category { Id = Guid.Parse("30875d78-2ac9-40c6-93b3-abd3789a2d3f"), Name = "Fashion" },
        new Category { Id = Guid.Parse("ecf907e7-2bce-430e-bff0-892be16e698c"), Name = "Auto" }
    };
}
