using Core.Enums;

namespace Core.Entities
{
    public class Report
    {
        public long ReportId { get; set; }
        public Frequency ReportType { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal NetBalance { get; set; } //saldo liquido
        public decimal EconomyRate { get; set; }
        public DateOnly GenerationDate { get; set; }

       public ICollection<ReportCategory> Categories { get; set; } = [];



    }
}
