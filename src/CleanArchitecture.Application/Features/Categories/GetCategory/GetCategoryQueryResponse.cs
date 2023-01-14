namespace CleanArchitecture.Application.Features.Categories.GetCategory;

public class GetCategoryQueryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}
