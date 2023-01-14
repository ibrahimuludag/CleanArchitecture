using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Validation;
using CleanArchitecture.Domain.Events;

namespace CleanArchitecture.Application.Features.Products.DeleteProduct;

public class DeleteProductCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; set; }
}

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<Unit>>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<Unit>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(request.Id);

        if (product == null)
        {
            return Result.Fail(new NotFoundError($"Product with id {request.Id} cannot be found"));
        }

        product.AddDomainEvent(EntityDeletedEvent.WithEntity(product));
        await _productRepository.Delete(product);
        
        return Result.Ok(Unit.Value);
    }
}