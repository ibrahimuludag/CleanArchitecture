using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Persistence.Context;

namespace CleanArchitecture.Persistence.Repositories;

public class BaseRepository<T> : IAsyncRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext _dbContext;

    public BaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<T?> GetById(Guid id)
    {
        T? t = await _dbContext.Set<T>().FindAsync(id);
        return t;
    }

    public virtual async Task<bool> Exists(Guid id)
    {
        bool exists = await _dbContext.Set<T>().AnyAsync(c => c.Id == id);
        return exists;
    }

    public async Task<IReadOnlyList<T>> ListAll()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async virtual Task<PaginatedList<T>> GetPagedReponse(int page, int size)
    {
        var queryable = _dbContext.Set<T>().AsQueryable();
        var items = await queryable.Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
        var count = await queryable.CountAsync();
        return new PaginatedList<T>(items, count, page, size);
    }

    public async Task<T> Add(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task Update(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}