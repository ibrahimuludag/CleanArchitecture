using CleanArchitecture.Application.Contracts.Persistence;

namespace CleanArchitecture.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(50);
    }
}
