using CleanArchitecture.Application.Contracts.Persistence;

namespace CleanArchitecture.Application.Features.Categories.ChangeName;

public class ChangeNameCommandValidator : AbstractValidator<ChangeNameCommand>
{
    public ChangeNameCommandValidator()
    {
        RuleFor(p => p.Id)
            .NotNull()
            .NotEqual(Guid.Empty);

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(250);
    }
}
