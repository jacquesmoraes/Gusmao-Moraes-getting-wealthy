namespace Core.Entities
{
    public class InstallmentExpense
    {
        public long InstallmentExpenseId { get; set; }
        public int NumberOfInstallments { get; set; }
        public decimal InstallmentAmount { get; set; }
        public DateOnly FirstInstallmentDate { get; set; }

        public ICollection<InstallmentExpenseItem> Items { get; set; } = [];
        public Expense? Expense { get; set; }
        


    }
}
