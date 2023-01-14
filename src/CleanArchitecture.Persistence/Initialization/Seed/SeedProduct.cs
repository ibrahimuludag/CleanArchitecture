using CleanArchitecture.Persistence.Context;
using System.Transactions;

namespace CleanArchitecture.Persistence.Initialization.Seed;

public class SeedProduct : ICustomSeeder
{
    private readonly ApplicationDbContext _context;
    
    private static object _locker = new object();

    public bool IsDevelopmentData => true;
    public int Order => 2;

    public SeedProduct(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task Initialize()
    {
        lock (_locker) {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (var product in SeedData.Products)
                {
                    _context.SeedData(product).GetAwaiter().GetResult();
                }

                _context.SaveChangesAsync().GetAwaiter().GetResult();
            }
        }
        
        return Task.CompletedTask;
    }
}