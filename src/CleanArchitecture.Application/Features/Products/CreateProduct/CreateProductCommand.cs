using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Validation;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Events;
using CleanArchitecture.Domain.ValueObjects;
using Mapster;

namespace CleanArchitecture.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<Result<CreateProductCommandResponse>>
{
    public string Title { get; set; } = default!;
    
    public string Sku { get; set; } = default!;
    
    public Guid? CategoryId { get; set; }
    
    public decimal? Price { get; set; }
    
    public decimal? Stock { get; set; }
    
    public Status Status { get; set; }
    
    public Weight? Weight { get; set; }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<CreateProductCommandResponse>>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public CreateProductCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<CreateProductCommandResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product { Id = Guid.NewGuid() };
        request.Adapt(product);

        if (request.CategoryId.HasValue) {
            var category = await _categoryRepository.GetById(request.CategoryId.Value);

            if (category == null)
            {
                return Result.Fail(new NotFoundError($"Category with id {request.CategoryId} cannot be found"));
            }
            product.Category = category;
        }

        product.AddDomainEvent(EntityCreatedEvent.WithEntity(product));
        await _productRepository.Add(product);
        var response = product.Adapt<CreateProductCommandResponse>();

        return Result.Ok(response);
    }
}