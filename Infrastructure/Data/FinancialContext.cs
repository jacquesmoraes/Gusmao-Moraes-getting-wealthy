using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class FinancialContext ( DbContextOptions<FinancialContext> options ) : DbContext( options )
    {
        public DbSet<Box> Boxes => Set<Box> ( );
        public DbSet<BoxContribute> BoxContributes => Set<BoxContribute> ( );
        public DbSet<BoxWithdraw> BoxWithdraws => Set<BoxWithdraw> ( );
        public DbSet<Budget> Budgets => Set<Budget> ( );
        public DbSet<BudgetCategory> BudgetCategories => Set<BudgetCategory> ( );
        public DbSet<Category> Categories => Set<Category> ( );
        public DbSet<Expense> Expenses => Set<Expense> ( );
        public DbSet<Income> Incomes => Set<Income> ( );
        public DbSet<InstallmentExpense> InstallmentExpenses => Set<InstallmentExpense> ( );
        public DbSet<InstallmentExpenseItem> InstallmentExpenseItems => Set<InstallmentExpenseItem> ( );
        public DbSet<RecurringExpense> RecurringExpenses => Set<RecurringExpense> ( );
        public DbSet<RecurringIncome> RecurringIncomes => Set<RecurringIncome> ( );
        public DbSet<Report> Reports => Set<Report> ( );
        public DbSet<ReportCategory> ReportItems => Set<ReportCategory> ( );
        public DbSet<WeeklyBudget> WeeklyBudgets => Set<WeeklyBudget> ( );

    }
}
