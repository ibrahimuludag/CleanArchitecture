using CleanArchitecture.Application.Features.Products.Commands.CreateProduct;

namespace CleanArchitecture.Application.Features.Products.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(p => p.Sku)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(p => p.CategoryId)
            .NotNull();

        RuleFor(p => p.Price)
            .NotNull()
            .GreaterThanOrEqualTo(0);
        
        RuleFor(p => p.Status)
            .NotNull();
    }
}
