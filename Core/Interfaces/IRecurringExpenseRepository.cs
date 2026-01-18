using Core.Entities;

namespace Core.Interfaces
{
     public interface IRecurringExpenseRepository
    {
        Task<RecurringExpense> CreateRecurringExpenseAsync ( RecurringExpense recurringExpense );
        Task<IEnumerable<RecurringExpense>> GetAllRecurringExpensesAsync ( );
        Task<RecurringExpense?> GetRecurringExpenseByIdAsync ( long id );
        Task<RecurringExpense> UpdateRecurringExpenseAsync ( RecurringExpense recurringExpense );
        Task<RecurringExpense?> DeleteRecurringExpenseAsync ( long id );
        Task<IEnumerable<RecurringExpense>> GetActiveRecurringExpensesDueAsync(DateOnly date);
    }
}
