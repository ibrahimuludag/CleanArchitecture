using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.Features.Products.GetPagedProducts;

public class GetPagedProductsQueryResponse
{
    public Guid Id { get; set; }

    public string Title { get; set; } = default!;

    public string Sku { get; set; } = default!;

    public Category? Category { get; set; }

    public decimal? Price { get; set; }

    public decimal? Stock { get; set; }

    public Status? Status { get; set; }

    public Weight? Weight { get; set; }
}
