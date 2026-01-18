using Applications.Dtos.ExpenseDtos;
using Core.Enums;

namespace Applications.Dtos.RecurringExpenseDto
{
    public class RecurringExpenseResponseDto
    {
        public long RecurringExpenseId { get; set; }
        public Frequency RecurringFrequency { get; set; }
        public int Day { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public bool IsActive { get; set; }
        public DateOnly NextDate { get; set; }
        public DateOnly CreationDate { get; set; }
        public ExpenseResponseDto? ExpenseResponse { get; set; }
    }
}
