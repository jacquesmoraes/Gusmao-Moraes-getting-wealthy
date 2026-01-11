using Core.Enums;

namespace Core.Entities
{
    public class BudgetCategory
    {
        public long BudgetCategoryId { get; set; }
        public decimal BudgetedAmount { get; set; }
        public decimal SpentAmount { get; set; }
        public decimal AvaliableBalance { get; set; }
        public decimal PercentUsage { get; set; }
        public BudgetStatus BudgetStatus { get; set; }
        public DateOnly CreationDate { get; set; }

        public long? BudgetId { get; set; }
        public  Budget? Budget { get; set; }

        public long? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
