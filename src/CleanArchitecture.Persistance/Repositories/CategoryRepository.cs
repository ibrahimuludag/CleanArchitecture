using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Persistance.Context;

namespace CleanArchitecture.Persistance.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }   
}