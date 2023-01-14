using CleanArchitecture.Application.Contracts.Persistence;

namespace CleanArchitecture.Application.Features.Products.DeleteProduct;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
	public DeleteProductCommandValidator()
    {
        RuleFor(p => p.Id)
            .NotNull()
            .NotEqual(Guid.Empty);
    }
}
