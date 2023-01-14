using CleanArchitecture.Application.Contracts.Persistence;
using Mapster;

namespace CleanArchitecture.Application.Features.Categories.Queries.GetCategoriesList;

public class GetCategoriesQuery : IRequest<Result<IEnumerable<GetCategoriesQueryResponse>>>
{
}

public class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesQuery, Result<IEnumerable<GetCategoriesQueryResponse>>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoriesListQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<IEnumerable<GetCategoriesQueryResponse>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = (await _categoryRepository.ListAll()).OrderBy(x => x.Name);
        var response = categories.Adapt<IEnumerable<GetCategoriesQueryResponse>>();
        
        return Result.Ok(response);
    }
}