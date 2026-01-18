using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure ( EntityTypeBuilder<Expense> builder )
        {
            builder.ToTable ( "Expenses" );
            builder.HasKey ( e => e.ExpenseId );
            builder.Property(e => e.ExpenseId)
                .HasColumnName("ExpenseId")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Description).HasMaxLength(200);
            builder.Property(e => e.Amount).HasPrecision(18, 2).IsRequired();
            builder.Property(e => e.Date).HasColumnType("date").IsRequired();
            builder.Property(e => e.ExpensePaymentMethod).IsRequired();
            builder.Property(e => e.Observations).HasMaxLength(200).HasDefaultValue(string.Empty);
            builder.Property(e => e.CreationDate).HasColumnType("date").IsRequired();

            builder.ToTable(t => t.HasCheckConstraint ( "CK_Expense_Amount_NonNegative", "\"Amount\" > 0" ) );

            builder.HasOne(c => c.InstallmentExpense)
                   .WithOne ( ie => ie.Expense )
                   .HasForeignKey<Expense> ( e => e.InstallmentExpenseId )
                   .OnDelete ( DeleteBehavior.Restrict );

            builder.HasOne( c => c.RecurringExpense )
                   .WithOne ( re => re.Expense )
                   .HasForeignKey<Expense> ( e => e.RecurringExpenseId )
                   .OnDelete ( DeleteBehavior.Restrict );


            //indexes
            builder.HasIndex(c => c.InstallmentExpenseId ).IsUnique().HasFilter("\"InstallmentExpenseId\" IS NOT NULL");
            builder.HasIndex( c => c.RecurringExpenseId ).IsUnique ( ).HasFilter ( "\"RecurringExpenseId\" IS NOT NULL" );
            builder.HasIndex( c => c.CategoryId );
            builder.HasIndex( c => c.ExpensePaymentMethod );
            builder.HasIndex( c => c.Date );
            builder.HasIndex(e => new{e.Date, e.CategoryId});


        }
    }
}
