using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class BoxWithdrawConfiguration : IEntityTypeConfiguration<BoxWithdraw>
    {
        public void Configure ( EntityTypeBuilder<BoxWithdraw> builder )
        {
            builder.ToTable("BoxWithdraws");
            builder.HasKey(bw => bw.BoxWithdrawId);

            builder.Property(bw => bw.BoxWithdrawId)
                .HasColumnName("BoxWithdrawId")
                .ValueGeneratedOnAdd();

            builder.Property(bw => bw.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(bw => bw.Date)
                .IsRequired();

            builder.Property(bw => bw.Description)
                .IsRequired();

            builder.Property(bw => bw.CreationDate)
                .IsRequired();

            builder.Property(bw => bw.BoxId)
                .IsRequired();

            builder.HasOne(bw => bw.Box)
                .WithMany(b => b.Withdraw)
                .HasForeignKey(bw => bw.BoxId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(bw => bw.Expense)
                .WithOne()
                .HasForeignKey<BoxWithdraw>(bw => bw.ExpenseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(bw => bw.ExpenseId)
                .IsUnique()
                .HasFilter("\"ExpenseId\" IS NOT NULL");

            builder.HasIndex(bw => bw.BoxId);
            builder.HasIndex(bw => bw.Date);
        }
    }
}
