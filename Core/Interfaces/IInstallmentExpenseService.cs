using Core.Entities;

namespace Core.Interfaces
{
    public interface IInstallmentExpenseService
    {
        Task<InstallmentExpense> CreateInstallmentExpense ( InstallmentExpense installmentExpense );

        Task<InstallmentExpense?> GetInstallmentExpenseById ( long id );
        Task<IEnumerable<InstallmentExpense>> GetAllInstallmentExpenses ( );
        Task<InstallmentExpense> UpdateInstallmentExpense ( InstallmentExpense installmentExpense );
        Task<InstallmentExpense?> DeleteInstallmentExpense ( long id );
    }
}
