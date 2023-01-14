using CleanArchitecture.Persistence.Context;
using System.Transactions;

namespace CleanArchitecture.Persistence.Initialization.Seed;

public class SeedCategory : ICustomSeeder
{
    private readonly ApplicationDbContext _context;

    private static object _locker = new object();

    public bool IsDevelopmentData => false;
    public int Order => 1;
    
    public SeedCategory(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task Initialize()
    {
        lock (_locker)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (var product in SeedData.Categories)
                {
                    _context.SeedData(product).GetAwaiter().GetResult();
                }

                _context.SaveChangesAsync().GetAwaiter().GetResult(); 
            }
        }

        return Task.CompletedTask;
    }
}