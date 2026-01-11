namespace Core.Entities
{
    //Regras de Negócio:
    //- Status OK: percentual <= 80%-
    //Status ATENCAO: 80% < percentual <= 100%-
    //Status EXCEDIDO: percentual > 100%- Alertas são gerados para ATENCAO e EXCEDIDO
    public class WeeklyBudget
    {
        public long WeeklyBudgetId { get; set; }
        public int Week { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal TotalBudget { get; set; }
        public decimal TotalSpent { get; set; }
        public decimal WeekBalance { get; set; }
        public DateOnly CreationDate { get; set; }

        public long? BudgetId { get; set; }
        public Budget? Budget { get; set; }
    }
}
