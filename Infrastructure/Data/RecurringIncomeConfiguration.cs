using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class RecurringIncomeConfiguration : IEntityTypeConfiguration<RecurringIncome>
    {
        public void Configure ( EntityTypeBuilder<RecurringIncome> builder )
        {
            
            builder.ToTable("RecurringIncomes");

            builder.HasKey(ri => ri.RecurringIncomeId);

            builder.Property(ri => ri.RecurringIncomeId)
                .HasColumnName("RecurringIncomeId")
                .ValueGeneratedOnAdd();

            builder.Property(ri => ri.RecurringFrequency)
                .IsRequired();

            builder.Property(ri => ri.Day)
                .IsRequired();

            builder.Property(ri => ri.StartDate)
                .IsRequired();

            builder.Property(ri => ri.EndDate);

            builder.Property(ri => ri.IsActive)
                .HasDefaultValue(true);

            builder.Property(ri => ri.NextDate)
                .IsRequired();

            builder.Property(ri => ri.CreationDate)
                .IsRequired();

            // Validações
            builder.ToTable(t => t.HasCheckConstraint("CK_RecurringIncome_Day_Range", "\"Day\" >= 1 AND \"Day\" <= 31"));

            //indexes
            builder.HasIndex(ri => ri.IsActive);
            builder.HasIndex(ri => ri.NextDate);
            builder.HasIndex(ri => ri.RecurringFrequency);

            
            builder.HasIndex(ri => new { ri.IsActive, ri.NextDate });
        }
    }
}
