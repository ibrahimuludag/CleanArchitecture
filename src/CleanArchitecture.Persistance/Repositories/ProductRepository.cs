using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Persistance.Context;

namespace CleanArchitecture.Persistance.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}