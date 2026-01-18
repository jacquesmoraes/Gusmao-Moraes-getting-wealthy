namespace Applications.Dtos.InstallmentDtos
{
    public class InstallmentExpenseResponseDto
    {
        public long InstallmentExpenseId { get; set; }
        public int NumberOfInstallments { get; set; }
        public decimal InstallmentAmount { get; set; }
        public DateOnly FirstInstallmentDate { get; set; }
        public long? ExpenseId { get; set; }
        public IEnumerable<InstallmentExpenseItemDto> Items { get; set; } = [];
    }
}
