using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Contracts.Persistence;

public interface IProductRepository : IAsyncRepository<Product>
{
}
