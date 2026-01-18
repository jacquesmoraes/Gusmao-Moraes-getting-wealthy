using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class IncomeConfiguration : IEntityTypeConfiguration<Income>
    {
        public void Configure ( EntityTypeBuilder<Income> builder )
        {
            builder.ToTable( "Incomes" );
            builder.HasKey(i => i.IncomeId);
            builder.Property(i => i.IncomeId)
                   .HasColumnName("IncomeId")
                   .ValueGeneratedOnAdd();

            builder.Property(i => i.Description).HasMaxLength(200);
            builder.Property(i => i.Amount).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(i => i.Date).IsRequired();
            builder.Property(i => i.IncomeMethod).IsRequired();
            builder.Property(i => i.AlreadyReceived).HasDefaultValue(false);
            builder.Property(i => i.Notes).HasMaxLength(200).HasDefaultValue(string.Empty);
            builder.Property(i => i.CreationDate).IsRequired();

            builder.ToTable(t => t.HasCheckConstraint("CK_Income_Amount_Positive", "\"Amount\" > 0"));

            builder.HasOne(i => i.RecurringIncome)
                   .WithOne(ri => ri.Income)
                   .HasForeignKey<Income>(i => i.RecurringIncomeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(i => i.RecurringIncomeId)
                   .IsUnique()
                   .HasFilter( "\"RecurringIncomeId\" IS NOT NULL" );

            builder.HasIndex(i => i.Date);
            builder.HasIndex(i => i.CategoryId);
            builder.HasIndex(i => i.IncomeMethod);
            builder.HasIndex(i => i.AlreadyReceived);
            builder.HasIndex(i => new{i.Date, i.CategoryId});
            
        }
    }
}
