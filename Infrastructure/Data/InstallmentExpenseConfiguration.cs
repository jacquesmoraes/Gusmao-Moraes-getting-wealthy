using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class InstallmentExpenseConfiguration : IEntityTypeConfiguration<InstallmentExpense>
    {
        public void Configure ( EntityTypeBuilder<InstallmentExpense> builder )
        {
            
            builder.ToTable("InstallmentExpenses");
            builder.HasKey(ie => ie.InstallmentExpenseId);

            builder.Property(ie => ie.InstallmentExpenseId)
                .HasColumnName("InstallmentExpenseId")
                .ValueGeneratedOnAdd();

            builder.Property(ie => ie.NumberOfInstallments)
                .IsRequired();

            builder.Property(ie => ie.InstallmentAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(ie => ie.FirstInstallmentDate)
                .IsRequired();

            
            builder.ToTable(t => t.HasCheckConstraint("CK_InstallmentExpense_NumberOfInstallments_Positive", "\"NumberOfInstallments\" > 0"));
            builder.ToTable(t => t.HasCheckConstraint("CK_InstallmentExpense_InstallmentAmount_Positive", "\"InstallmentAmount\" > 0"));
                        
            builder.HasMany(ie => ie.Items)
                .WithOne(iei => iei.InstallmentExpense)
                .HasForeignKey(iei => iei.InstallmentExpenseId)
                .OnDelete(DeleteBehavior.Cascade); 

            // Índices 
            builder.HasIndex(ie => ie.FirstInstallmentDate);
       
        }
    }
}
