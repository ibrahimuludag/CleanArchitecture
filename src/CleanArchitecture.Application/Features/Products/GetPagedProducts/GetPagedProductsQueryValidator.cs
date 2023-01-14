namespace CleanArchitecture.Application.Features.Products.GetPagedProducts;

public class GetPagedProductsQueryValidator : AbstractValidator<GetPagedProductsQuery>
{
    public GetPagedProductsQueryValidator()
    {
        RuleFor(p => p.PageNumber)
            .NotNull()
            .GreaterThan(0);

        RuleFor(p => p.PageSize)
            .NotNull()
            .GreaterThan(0);
    }
}