using Core.Enums;

namespace Applications.Dtos.ExpenseDtos
{
   
    public class CreateExpenseDto
    {
         public string Description { get; set; } =string.Empty;
        public decimal Amount { get; set; }
        public DateOnly Date { get; set; }
        public PaymentMethod ExpensePaymentMethod { get; set; }
        public string Observations { get; set; } = string.Empty;
        public long CategoryId { get; set; }
    }
}
