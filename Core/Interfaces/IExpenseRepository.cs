using Core.Entities;

namespace Core.Interfaces
{
     public interface IExpenseRepository
    {
          Task<Expense> CreateExpenseAsync ( Expense expense );
        Task<IEnumerable<Expense>> GetAllExpensesAsync (long? categoryId = null, DateOnly? startDate = null, DateOnly? endDate = null );
        Task<Expense?> GetExpenseByIdAsync ( long id );
        Task<Expense> UpdateExpenseAsync ( Expense expense );
        Task<Expense?> DeleteExpenseAsync ( long id );
        
    }
}
