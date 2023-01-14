namespace CleanArchitecture.Application.Features.Categories.Queries.GetCategoriesList;

public class GetCategoriesQueryResponse
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
}