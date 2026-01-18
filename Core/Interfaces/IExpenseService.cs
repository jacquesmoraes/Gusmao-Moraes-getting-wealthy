using Core.Entities;

namespace Core.Interfaces
{
    public interface IExpenseService
    {
        Task<Expense> CreateExpense ( Expense expense );
        Task<IEnumerable<Expense>> GetAllExpenses (long? categoryId = null, DateOnly? startDate = null, DateOnly? endDate = null  );
        Task<Expense?> GetExpenseById ( long id );
        Task<Expense?> UpdateExpense ( long id, Expense expense );
        Task<Expense?> DeleteExpense ( long id );
           }
}
