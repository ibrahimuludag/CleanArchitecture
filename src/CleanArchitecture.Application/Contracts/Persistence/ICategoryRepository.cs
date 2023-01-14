using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Contracts.Persistence;

public interface ICategoryRepository : IAsyncRepository<Category>
{
}