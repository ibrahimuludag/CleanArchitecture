using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.Categories.Queries.GetCategoriesList;

namespace CleanArchitecture.Application.Features.Categories.GetCategory;

public class GetCategoryQueryValidator : AbstractValidator<GetCategoryQuery>
{
    public GetCategoryQueryValidator()
    {
        RuleFor(p => p.Id)
            .NotNull()
            .NotEqual(Guid.Empty); 
    }
}