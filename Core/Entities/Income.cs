using Core.Enums;

namespace Core.Entities
{
    public class Income
    {
        public long IncomeId { get; set; }
        public string Description { get; set; } =string.Empty;
        public decimal Amount { get; set; }
        public DateOnly Date { get; set; }
        public PaymentMethod IncomeMethod { get; set; }
        public bool AlreadyReceived { get; set; }
        public string Notes { get; set; } = string.Empty;
        public DateOnly CreationDate { get; set; }



        public long? RecurringIncomeId { get; set; }
        public RecurringIncome? RecurringIncome { get; set; }

        public required long CategoryId{ get; set; }
        public required Category Category { get; set; }

    }
}
