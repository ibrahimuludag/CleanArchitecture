using CleanArchitecture.Application.Contracts.Persistence;

namespace CleanArchitecture.Application.Features.Categories.DeleteCategory;

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(p => p.Id)
            .NotNull()
            .NotEqual(Guid.Empty);
    }
}