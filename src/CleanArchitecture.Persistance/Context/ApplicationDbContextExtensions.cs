using CleanArchitecture.Domain.Common;
using Mapster;

namespace CleanArchitecture.Persistance.Context;

public static class ApplicationDbContextExtensions
{
    public static async Task SeedData<T>(this ApplicationDbContext context, T data) where T : BaseEntity
    {
        var set = context.Set<T>();
        var entity = await set.FindAsync(data.Id);
        
        if (entity is null)
        {
            set.Add(data);
        }
        else {
            data.Adapt(entity);
            set.Update(entity);
        }
    }
}
