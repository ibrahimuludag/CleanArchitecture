using CleanArchitecture.Application.Contracts.Persistence;

namespace CleanArchitecture.Application.Features.Products.SetProductStock;

public class SetProductStockCommandValidator : AbstractValidator<SetProductStockCommand>
{
    public SetProductStockCommandValidator()
    {
        RuleFor(p => p.Id)
            .NotNull()
            .NotEqual(Guid.Empty);

        RuleFor(p => p.Stock)
            .NotNull()
            .GreaterThan(0);
    }
}
