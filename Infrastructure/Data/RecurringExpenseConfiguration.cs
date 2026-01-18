using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class RecurringExpenseConfiguration : IEntityTypeConfiguration<RecurringExpense>
    {
        public void Configure ( EntityTypeBuilder<RecurringExpense> builder )
        {
             // Nome da tabela
            builder.ToTable("RecurringExpenses");
            builder.HasKey(re => re.RecurringExpenseId);

            builder.Property(re => re.RecurringExpenseId)
                .HasColumnName("RecurringExpenseId")
                .ValueGeneratedOnAdd();

            builder.Property(re => re.RecurringFrequency)
                .IsRequired();

            builder.Property(re => re.Day)
                .IsRequired();

            builder.Property(re => re.StartDate)
                .IsRequired();

            builder.Property(re => re.EndDate);

            builder.Property(re => re.IsActive)
                .HasDefaultValue(true);

            builder.Property(re => re.NextDate)
                .IsRequired();

            builder.Property(re => re.CreationDate)
                .IsRequired();

            // Validações
            builder.ToTable(t => t.HasCheckConstraint("CK_RecurringExpense_Day_Range", "\"Day\" >= 1 AND \"Day\" <= 31"));

            //indices
            builder.HasIndex(re => re.IsActive);
            builder.HasIndex(re => re.NextDate);
            builder.HasIndex(re => re.RecurringFrequency);

            
            builder.HasIndex(re => new { re.IsActive, re.NextDate });
        }
    }
}
