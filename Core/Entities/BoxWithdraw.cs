namespace Core.Entities
{
    public class BoxWithdraw
    {
        public long BoxWithdrawId { get; set; }
        public decimal Amount { get; set; }
        public DateOnly Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateOnly CreationDate { get; set; }

        public long BoxId { get; set; }
        public Box? Box { get; set; }


        public long? ExpenseId { get; set; }
        public Expense? Expense { get; set; }
    }
}
