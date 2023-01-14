namespace CleanArchitecture.Application.Features.Categories.CreateCategory;

public class CreateCategoryCommandResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}
