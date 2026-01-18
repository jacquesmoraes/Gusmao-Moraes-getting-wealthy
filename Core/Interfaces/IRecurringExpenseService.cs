using Core.Entities;

namespace Core.Interfaces
{
    public interface IRecurringExpenseService
    {
        Task<RecurringExpense> CreateRecurringExpense ( RecurringExpense recurringExpense );
        Task<IEnumerable<RecurringExpense>> GetAllRecurringExpenses ( );
        Task<RecurringExpense?> GetRecurringExpenseById ( long id );
        Task<RecurringExpense> UpdateRecurringExpense ( RecurringExpense recurringExpense );
        Task<RecurringExpense?> DeleteRecurringExpense ( long id );
        Task<RecurringExpense?> ToggleActive(long id) ;
        Task<IEnumerable<RecurringExpense>> GetActiveRecurringExpensesDue(DateOnly date);
    }
}
