using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Features.Categories.CreateCategory;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events;
using Mapster;

namespace CleanArchitecture.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest<Result<CreateCategoryCommandResponse>>
{
    public string Name { get; set; } = default!;
}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<CreateCategoryCommandResponse>>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<CreateCategoryCommandResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category { Id = Guid.NewGuid() };
        request.Adapt(category);
        
        category.AddDomainEvent(EntityCreatedEvent.WithEntity(category));
        category = await _categoryRepository.Add(category);

        var response = category.Adapt<CreateCategoryCommandResponse>();

        return Result.Ok(response);
    }
}