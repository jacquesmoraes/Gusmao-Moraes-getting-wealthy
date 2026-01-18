using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class InstallmentExpenseRepository ( FinancialContext context ) : IInstallmentExpenseRepository
    {
        private readonly FinancialContext _context = context;

        public async Task<InstallmentExpense> CreateInstallmentExpenseAsync ( InstallmentExpense installmentExpense )
        {
            _context.InstallmentExpenses.Add ( installmentExpense );
            await _context.SaveChangesAsync ( );
            return installmentExpense;
        }

        public async Task<InstallmentExpense?> GetInstallmentExpenseByIdAsync ( long id )
        {
            return await _context.InstallmentExpenses
                .Include ( ie => ie.Items )
                .Include ( ie => ie.Expense )
                .ThenInclude ( e => e.Category )
                .FirstOrDefaultAsync ( ie => ie.InstallmentExpenseId == id );
        }

        public async Task<IEnumerable<InstallmentExpense>> GetAllInstallmentExpensesAsync ( )
        {
            return await _context.InstallmentExpenses
                .Include ( ie => ie.Items )
                .Include ( ie => ie.Expense )
                .ThenInclude ( e => e.Category )
                .OrderByDescending ( ie => ie.FirstInstallmentDate )
                .ToListAsync ( );
        }

        public async Task<InstallmentExpense> UpdateInstallmentExpenseAsync ( InstallmentExpense installmentExpense )
        {
            _context.InstallmentExpenses.Update ( installmentExpense );
            await _context.SaveChangesAsync ( );
            return installmentExpense;
        }

        public async Task<InstallmentExpense?> DeleteInstallmentExpenseAsync ( long id )
        {
            var installmentExpense = await _context.InstallmentExpenses
                .Include ( ie => ie.Items )
                .Include ( ie => ie.Expense )
                .FirstOrDefaultAsync ( ie => ie.InstallmentExpenseId == id );

            if ( installmentExpense == null )
            {
                return null;
            }

            _context.InstallmentExpenses.Remove ( installmentExpense );
            await _context.SaveChangesAsync ( );
            return installmentExpense;
        }
    }
}
