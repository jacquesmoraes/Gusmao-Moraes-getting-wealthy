namespace Core.Entities
{
    //Regras de Negócio:
    //- Gastos reais = despesas do orçamento normal(não inclui despesas pagas com Caixa)- Saldo disponível = Total - Gasto Real - Aportes- Orçamento semanal é calculado como 25% do mensal
    public class Budget
    {
        public long BudgetId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public required string VisualType { get; set; } // weekly, monthly
        public int? Week { get; set; }
        public decimal Total { get; set; }
        public decimal RealExpense { get; set; }
        public decimal BoxContribute { get; set; }
        public decimal AvailableBalance { get; set; }
        public DateOnly CreationDate { get; set; }
        public DateOnly UpdateDate { get; set; }
        public ICollection<BudgetCategory> Categories { get; set; } = [];
        public ICollection<WeeklyBudget> WeeklyBudgets { get; set; } = [];
        

    }
}
