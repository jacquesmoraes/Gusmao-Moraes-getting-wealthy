
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure ( EntityTypeBuilder<Category> builder )
        {
            builder.ToTable ( "Categories" );
            builder.HasKey( c => c.CategoryId );
            builder.Property( c => c.Name ).IsRequired().HasMaxLength( 100 );
            builder.Property( c => c.Icon ).HasMaxLength( 100 ).HasDefaultValue( string.Empty );
            builder.Property( c => c.Color ).HasMaxLength( 100 ).HasDefaultValue( string.Empty );
            builder.Property( c => c.Type ).IsRequired();
            builder.Property( c => c.Description ).HasMaxLength( 200 ).HasDefaultValue( string.Empty );
            builder.Property( c => c.IsFavorite ).HasDefaultValue( false );
            builder.Property( c => c.IsActive ).HasDefaultValue( true );
            builder.Property( c => c.CreationDate ).IsRequired();

            builder.HasMany(c => c.Expenses)
                   .WithOne(e => e.Category)
                   .HasForeignKey(e => e.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Incomes)
                   .WithOne(i => i.Category)
                   .HasForeignKey(i => i.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany<BudgetCategory>()
                   .WithOne(bc => bc.Category)
                   .HasForeignKey(bc => bc.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasMany<ReportCategory>()
                   .WithOne ( rc => rc.Category )
                   .HasForeignKey ( rc => rc.CategoryId )
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasIndex(c => c.Type);
            builder.HasIndex(c => c.IsFavorite );
            builder.HasIndex ( c => c.IsActive );

        }
    }
}
