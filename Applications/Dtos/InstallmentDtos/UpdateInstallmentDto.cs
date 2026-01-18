using Core.Enums;

namespace Applications.Dtos.InstallmentDtos
{
    public class UpdateInstallmentDto
    {
         public  int NumberOfInstallments { get; set; }
        public string Description { get; set; } =string.Empty;
        public decimal Amount { get; set; }
        public DateOnly FisrtInstallmentDate { get; set; }
        public PaymentMethod ExpensePaymentMethod { get; set; }
        public string Observations { get;  set; } = string.Empty;
        public int CategoryId { get; set; }
    }
}
