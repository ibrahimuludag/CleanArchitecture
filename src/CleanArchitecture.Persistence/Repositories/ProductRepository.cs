using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Persistence.Context;

namespace CleanArchitecture.Persistence.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}