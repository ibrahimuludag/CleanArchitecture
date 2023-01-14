namespace CleanArchitecture.Persistance.Configurations;

public class CategoryConfiguration  : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category", "dbo");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(e => e.CreatedBy)
            .HasMaxLength(250);

        builder.Property(e => e.LastModifiedBy)
            .HasMaxLength(250);
    }
}
