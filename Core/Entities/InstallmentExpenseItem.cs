namespace Core.Entities
{
    public class InstallmentExpenseItem
    {
        public long InstallmentExpenseItemId { get; set; }
        public int InstallmentNumber { get; set; }    
        public decimal Value { get; set; }            
        public DateOnly DueDate { get; set; }         
        public DateOnly? PaymentDate { get; set; }    
        public bool IsPaid { get; set; }              
        public string MonthReference { get; set; } = string.Empty;

        public  long InstallmentExpenseId { get; set; }
        public InstallmentExpense? InstallmentExpense { get; set; }
    }
}
