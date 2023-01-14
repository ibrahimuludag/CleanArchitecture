using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.Categories.GetCategory;
using CleanArchitecture.Application.Validation;
using Mapster;

namespace CleanArchitecture.Application.Features.Categories.Queries.GetCategoriesList;

public class GetCategoryQuery : IRequest<Result<GetCategoryQueryResponse>>
{
    public Guid Id { get; set; }
}

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, Result<GetCategoryQueryResponse>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<GetCategoryQueryResponse>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetById(request.Id);
        
        if (category == null) {
            return Result.Fail(new NotFoundError($"Category with id {request.Id} cannot be found"));
        }
        
        var response = category.Adapt<GetCategoryQueryResponse>();
        
        return Result.Ok(response);
    }
}