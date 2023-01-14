using CleanArchitecture.Application.Contracts.Persistence;
using Mapster;

namespace CleanArchitecture.Application.Features.Products.Queries.GetProductsList;

public class GetProductsQuery : IRequest<Result<IEnumerable<GetProductsQueryResponse>>>
{
}

public class GetProductsListQueryHandler : IRequestHandler<GetProductsQuery, Result<IEnumerable<GetProductsQueryResponse>>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsListQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<IEnumerable<GetProductsQueryResponse>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = (await _productRepository.ListAll()).OrderBy(x => x.Title);
        
        var response = products.Adapt<IEnumerable<GetProductsQueryResponse>>();
        
        return Result.Ok(response);
    }
}