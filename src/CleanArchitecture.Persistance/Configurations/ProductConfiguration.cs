namespace CleanArchitecture.Persistance.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product", "dbo");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Price);
        builder.Property(e => e.Sku)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Stock)
            .HasPrecision(18, 2);

        builder.Property(e => e.Price)
            .HasPrecision(18, 4);

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(e => e.CreatedBy)
            .HasMaxLength(250);

        builder.Property(e => e.LastModifiedBy)
            .HasMaxLength(250);

        builder.OwnsOne(e => e.Weight);
    }
}