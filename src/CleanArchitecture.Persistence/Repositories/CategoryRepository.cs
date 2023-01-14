using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Persistence.Context;

namespace CleanArchitecture.Persistence.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }   
}