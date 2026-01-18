using Core.Enums;

namespace Applications.Dtos.RecurringExpenseDto
{
    public class UpdateRecurringExpenseDto
    {
        public required Frequency RecurringFrequency { get; set; }
        public required int Day { get; set; }
        public required DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public bool IsActive { get; set; } = true;
        public PaymentMethod ExpensePaymentMethod { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public long CategoryId { get; set; }
        public string Observations { get; set; } = string.Empty;
    }
}
