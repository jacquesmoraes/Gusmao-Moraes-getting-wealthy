using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class BoxConfiguration : IEntityTypeConfiguration<Box>
    {
        public void Configure ( EntityTypeBuilder<Box> builder )
        {
            
            builder.ToTable("Boxes");
                     
            builder.HasKey(b => b.BoxId);
            
            builder.Property(b => b.BoxId)
                   .HasColumnName("BoxId")
                   .ValueGeneratedOnAdd();

            builder.Property(b => b.BoxName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(b => b.BoxGoal)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(b => b.BudgetTarget)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(b => b.CurrentBalance)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(b => b.Icon)
                .HasMaxLength(50);

            builder.Property(b => b.Color)
                .HasMaxLength(20);

            builder.Property(b => b.StartDate)
                .IsRequired();

            builder.Property(b => b.EndDate)
                .IsRequired();

            builder.Property(b => b.Status)
                .IsRequired();

            builder.Property(b => b.CreationDate)
                .IsRequired();

            // Validações
            builder.ToTable(t => t.HasCheckConstraint("CK_Box_BudgetTarget_Positive", "\"BudgetTarget\" > 0"));
            builder.ToTable(t => t.HasCheckConstraint("CK_Box_CurrentBalance_NonNegative",  "\"CurrentBalance\" >= 0"));

            
            
            builder.HasMany(b => b.Contribute)
                .WithOne(bc => bc.Box)
                .HasForeignKey(bc => bc.BoxId)
                .OnDelete(DeleteBehavior.Cascade); 

            
            builder.HasMany(b => b.Withdraw)
                .WithOne(bw => bw.Box)
                .HasForeignKey(bw => bw.BoxId)
                .OnDelete(DeleteBehavior.Cascade); 

            // index 
            builder.HasIndex(b => b.Status);
            builder.HasIndex(b => b.EndDate);
            builder.HasIndex(b => b.CreationDate);

            
            builder.HasIndex(b => new { b.Status, b.EndDate });
        }
    }
}
