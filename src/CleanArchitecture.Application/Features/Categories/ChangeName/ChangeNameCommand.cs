using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Validation;

namespace CleanArchitecture.Application.Features.Categories.ChangeName;

public class ChangeNameCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}

public class ChangeCategoryNameCommandHandler : IRequestHandler<ChangeNameCommand, Result<Unit>>
{
    private readonly ICategoryRepository _categoryRepository;

    public ChangeCategoryNameCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<Unit>> Handle(ChangeNameCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetById(request.Id);

        if (category == null)
        {
            return Result.Fail(new NotFoundError($"Category with id {request.Id} cannot be found"));
        }

        category.Name = request.Name;
        await _categoryRepository.Update(category);

        return Result.Ok(Unit.Value);
    }
}