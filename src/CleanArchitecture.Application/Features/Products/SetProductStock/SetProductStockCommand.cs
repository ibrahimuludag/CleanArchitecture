using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Validation;

namespace CleanArchitecture.Application.Features.Products.SetProductStock;

public class SetProductStockCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; set; }
    
    public decimal Stock { get; set; }
}

public class SetProductCommandHandler : IRequestHandler<SetProductStockCommand, Result<Unit>>
{
    private readonly IProductRepository _productRepository;

    public SetProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<Unit>> Handle(SetProductStockCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(request.Id);

        if (product == null)
        {
            return Result.Fail(new NotFoundError($"Product with id {request.Id} cannot be found"));
        }

        product.Stock = request.Stock;
        await _productRepository.Update(product);

        return Result.Ok(Unit.Value);
    }
}
