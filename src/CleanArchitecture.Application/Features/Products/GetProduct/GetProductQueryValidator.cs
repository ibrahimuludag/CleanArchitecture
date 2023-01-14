using CleanArchitecture.Application.Contracts.Persistence;

namespace CleanArchitecture.Application.Features.Products.GetProduct;

public class GetProductQueryValidator : AbstractValidator<GetProductQuery>
{
	public GetProductQueryValidator(IProductRepository productRepository)
    {
        RuleFor(p => p.Id)
            .NotNull()
            .NotEqual(Guid.Empty);
    }
}