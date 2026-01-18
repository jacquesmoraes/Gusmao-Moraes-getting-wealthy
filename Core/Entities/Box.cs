using Core.Enums;

namespace Core.Entities
{
    //Regras de Negócio:- O saldo atual é calculado automaticamente baseado nos aportes menos as saídas
    //- Quando a meta é alcançada, status mudaautomaticamente para COMPLETA-
    //Aportes mensais são comprometidos ereduzem o saldo disponível do mês
    public class Box
    {
        public long BoxId { get; set; }
        public required string BoxName { get; set; }
        public required string BoxGoal { get; set; }
        public decimal BudgetTarget { get; set; }
        public decimal CurrentBalance { get; set; }
        public string? Icon { get; set; }
        public string? Color { get; set; }
        public DateOnly StartDate { get; set; } = new DateOnly();
        public DateOnly EndDate { get; set; }
        public BoxStatus Status { get; set; }
        public DateOnly CreationDate { get; set; }

        public ICollection<BoxContribute> Contribute { get; set; } = [];

        public ICollection<BoxWithdraw> Withdraw { get; set; } = [];
        

    }
}
