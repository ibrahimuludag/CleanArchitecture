using CleanArchitecture.Application.Contracts.Authorization;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Persistence.Common;
using MediatR;

namespace CleanArchitecture.Persistence.Context;

public class ApplicationDbContext : DbContext
{
    private readonly ICurrentUserService? _currentUserService;
    private readonly IMediator? _mediator;

    public ApplicationDbContext() : base()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService? currentUserService, IMediator? mediator)
       : base(options)
    {
        _currentUserService = currentUserService;
        _mediator = mediator;
    }

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Category> Categories => Set<Category>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=CleanArchitecture;Integrated Security=True;TrustServerCertificate=True;",
                   builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        await _mediator!.DispatchDomainEvents(this);

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTimeOffset.Now;
                    entry.Entity.CreatedBy = _currentUserService?.UserName ?? string.Empty;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTimeOffset.Now;
                    entry.Entity.LastModifiedBy = _currentUserService?.UserName ?? string.Empty;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}

