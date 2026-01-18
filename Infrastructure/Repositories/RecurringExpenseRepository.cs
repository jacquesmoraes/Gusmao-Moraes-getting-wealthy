using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RecurringExpenseRepository ( FinancialContext context ) : IRecurringExpenseRepository
    {
        private readonly FinancialContext _context = context;

        public async Task<RecurringExpense> CreateRecurringExpenseAsync ( RecurringExpense recurringExpense )
        {
            _context.RecurringExpenses.Add ( recurringExpense );
            await _context.SaveChangesAsync ( );
            return recurringExpense;
        }

        public async Task<IEnumerable<RecurringExpense>> GetAllRecurringExpensesAsync ( )
        {
            return await _context.RecurringExpenses
                .Include ( re => re.Expense )
                .ThenInclude ( e => e.Category )
                .OrderByDescending ( re => re.CreationDate )
                .ToListAsync ( );
        }

        public async Task<RecurringExpense?> GetRecurringExpenseByIdAsync ( long id )
        {
            return await _context.RecurringExpenses
                .Include ( re => re.Expense )
                .ThenInclude ( e => e.Category )
                .FirstOrDefaultAsync ( re => re.RecurringExpenseId == id );
        }

        public async Task<IEnumerable<RecurringExpense>> GetActiveRecurringExpensesDueAsync ( DateOnly date )
        {
            return await _context.RecurringExpenses
        .Include ( re => re.Expense )
        .ThenInclude ( e => e.Category )
        .Where ( re => re.IsActive
                    && re.NextDate <= date
                    && ( re.EndDate == null || re.EndDate.Value >= date ) )
        .OrderBy ( re => re.NextDate )
        .ToListAsync ( );
        }

        public async Task<RecurringExpense> UpdateRecurringExpenseAsync ( RecurringExpense recurringExpense )
        {
            _context.RecurringExpenses.Update ( recurringExpense );
            await _context.SaveChangesAsync ( );
            return recurringExpense;
        }

        public async Task<RecurringExpense?> DeleteRecurringExpenseAsync ( long id )
        {
            var recurringExpense = await _context.RecurringExpenses
                .Include ( re => re.Expense )
                .FirstOrDefaultAsync ( re => re.RecurringExpenseId == id );

            if ( recurringExpense == null )
            {
                return null;
            }

            _context.RecurringExpenses.Remove ( recurringExpense );
            await _context.SaveChangesAsync ( );
            return recurringExpense;
        }
    }
}
