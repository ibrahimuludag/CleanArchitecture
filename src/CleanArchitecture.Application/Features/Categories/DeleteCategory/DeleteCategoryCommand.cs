using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Validation;
using CleanArchitecture.Domain.Events;

namespace CleanArchitecture.Application.Features.Categories.DeleteCategory;

public class DeleteCategoryCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; set; }
}

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<Unit>>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<Unit>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetById(request.Id);
        
        if (category == null)
        {
            return Result.Fail(new NotFoundError($"Category with id {request.Id} cannot be found"));
        }

        category.AddDomainEvent(EntityDeletedEvent.WithEntity(category));
        await _categoryRepository.Delete(category);
        
        return Result.Ok(Unit.Value);
    }
}