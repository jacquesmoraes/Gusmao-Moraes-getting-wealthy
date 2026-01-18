namespace Applications.Dtos.InstallmentDtos
{
    public class InstallmentExpenseItemDto
    {
        public long InstallmentExpenseId { get; set; }
        public int InstallmentNumber { get; set; }
        public decimal Amount { get; set; }
        public DateOnly DueDate { get; set; }
        public DateOnly? PaymentDate { get; set; }
        public bool IsPaid { get; set; }
        public string MonthReference { get; set; } = string.Empty;
        public long InstallmentExpenseItemId { get; set; }
    }
}
