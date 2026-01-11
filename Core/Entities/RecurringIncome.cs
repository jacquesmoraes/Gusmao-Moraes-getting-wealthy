using Core.Enums;

namespace Core.Entities
{
    public class RecurringIncome
    {
        public long RecurringIncomeId { get; set; }
        public Frequency RecurringFrequency { get; set; }
        public int Day { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public bool IsActive { get; set; }
        public DateOnly NextDate { get; set; }
        public DateOnly CreationDate { get; set; }

        
        public  Income? Income { get; set; }



    }
}
