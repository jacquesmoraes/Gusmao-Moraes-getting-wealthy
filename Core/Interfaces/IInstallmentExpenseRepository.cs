using Core.Entities;

namespace Core.Interfaces
{
    public interface IInstallmentExpenseRepository
    {
        Task<InstallmentExpense> CreateInstallmentExpenseAsync ( InstallmentExpense installmentExpense );

        Task<InstallmentExpense?> GetInstallmentExpenseByIdAsync ( long id );
        Task<IEnumerable<InstallmentExpense>> GetAllInstallmentExpensesAsync ( );
        Task<InstallmentExpense> UpdateInstallmentExpenseAsync ( InstallmentExpense installmentExpense );
        Task<InstallmentExpense?> DeleteInstallmentExpenseAsync ( long id );
    }
}
