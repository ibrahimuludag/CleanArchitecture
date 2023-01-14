using CleanArchitecture.Application.Models;

namespace CleanArchitecture.Application.Contracts.Persistence;

public interface IAsyncRepository<T> where T : class
{
    Task<T?> GetById(Guid id);
    
    Task<bool> Exists(Guid id);

    Task<IReadOnlyList<T>> ListAll();
    
    Task<T> Add(T entity);
    
    Task Update(T entity);
    
    Task Delete(T entity);
    
    Task<PaginatedList<T>> GetPagedReponse(int page, int size);
}