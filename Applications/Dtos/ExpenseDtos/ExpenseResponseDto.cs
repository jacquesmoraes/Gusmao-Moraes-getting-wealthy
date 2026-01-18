using Core.Enums;

namespace Applications.Dtos.ExpenseDtos
{
    public class ExpenseResponseDto
    {
        public long ExpenseId { get; set; }
        public string Description { get; set; } =string.Empty;
        public decimal Amount { get; set; }
        public DateOnly Date { get; set; }
        public PaymentMethod ExpensePaymentMethod { get; set; }
        public string Observations { get; set; } = string.Empty;
        public DateOnly CreationDate { get; set; }
        public long CategoryId { get; set; }
         public string CategoryName { get; set; } = string.Empty;
        public string CategoryColor { get; set; } = string.Empty;
        public string CategoryIcon { get; set; } = string.Empty;
        public long? InstallmentExpenseId  { get; set; }
        public long? RecurringExpenseId { get; set; }
    }
}
