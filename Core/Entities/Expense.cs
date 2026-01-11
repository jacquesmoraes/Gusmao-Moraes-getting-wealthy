using Core.Enums;

namespace Core.Entities
{
    public class Expense
    {
        public long ExpenseId { get; set; }
        public string Description { get; set; } =string.Empty;
        public decimal Amount { get; set; }
        public DateOnly Date { get; set; }
        public PaymentMethod ExpensePaymentMethod { get; set; }
        public PaymentType Type { get; set; }
        public string Observations { get; set; } = string.Empty;
        public DateOnly CreationDate { get; set; }


        public long CategoryId { get; set; }
        public Category? Category { get; set; }

        public long? BoxId { get; set; }
        public Box? Box { get; set; }

        public long? InstallmentExpenseId  { get; set; }
        public InstallmentExpense? InstallmentExpense { get; set; }

        public long? RecurringExpenseId { get; set; }
        public RecurringExpense? RecurringExpense { get; set; }
    }
}
