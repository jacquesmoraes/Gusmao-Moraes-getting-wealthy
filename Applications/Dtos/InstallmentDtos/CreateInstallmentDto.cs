using Core.Enums;

namespace Applications.Dtos.InstallmentDtos
{
    public class CreateInstallmentDto
    {
        public required int NumberOfInstallments { get; set; }
        public string Description { get; set; } =string.Empty;
        public required decimal Amount { get; set; }
        public DateOnly FirstInstallmentDate { get; set; }
        public PaymentMethod ExpensePaymentMethod { get; set; }
        public string Observations { get;  set; } = string.Empty;
        public int CategoryId { get; set; }
    }
}
