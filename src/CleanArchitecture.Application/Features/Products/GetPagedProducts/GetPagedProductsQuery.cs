using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.Products.Queries.GetProductsList;
using CleanArchitecture.Application.Models;
using Mapster;

namespace CleanArchitecture.Application.Features.Products.GetPagedProducts;

public class GetPagedProductsQuery : IRequest<Result<PaginatedList<GetPagedProductsQueryResponse>>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class GetPagedProductsQueryHandler : IRequestHandler<GetPagedProductsQuery, Result<PaginatedList<GetPagedProductsQueryResponse>>>
{
    private readonly IProductRepository _productRepository;

    public GetPagedProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<PaginatedList<GetPagedProductsQueryResponse>>> Handle(GetPagedProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetPagedReponse(request.PageNumber, request.PageSize);

        var response = new PaginatedList<GetPagedProductsQueryResponse>(
            products.Items.Adapt<List<GetPagedProductsQueryResponse>>(), 
            products.TotalCount, 
            request.PageNumber, 
            request.PageSize);

        return Result.Ok(response);
    }
}