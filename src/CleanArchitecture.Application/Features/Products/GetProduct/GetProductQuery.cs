using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Validation;
using Mapster;

namespace CleanArchitecture.Application.Features.Products.GetProduct;

public class GetProductQuery : IRequest<Result<GetProductQueryResponse>>
{
    public Guid Id { get; set; }
}

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Result<GetProductQueryResponse>>
{
    private readonly IProductRepository _productRepository;

    public GetProductQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<GetProductQueryResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(request.Id);
        
        if (product == null) {
            return Result.Fail(new NotFoundError($"Product with id {request.Id} cannot be found"));
        }
        
        var response = product.Adapt<GetProductQueryResponse>();
        
        return Result.Ok(response);
    }
}