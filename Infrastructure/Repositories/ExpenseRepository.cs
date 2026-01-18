using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ExpenseRepository ( FinancialContext context ) : IExpenseRepository
    {
        private readonly FinancialContext _context = context;

        public async Task<Expense> CreateExpenseAsync ( Expense expense )
        {
            _context.Expenses.Add ( expense );
            await _context.SaveChangesAsync ( );
            return expense;
        }

        public async Task<IEnumerable<Expense>> GetAllExpensesAsync ( long? categoryId = null, DateOnly? startDate = null, DateOnly? endDate = null )
        {
            var query = _context.Expenses.Include(e => e.Category).AsQueryable();

            if ( categoryId.HasValue )
            {
                query = query.Where ( e => e.CategoryId == categoryId.Value );
            }

            if ( startDate.HasValue )
            {
                query = query.Where ( e => e.Date >= startDate.Value );
            }

            if ( endDate.HasValue )
            {
                query = query.Where ( e => e.Date <= endDate.Value );
            }

            return await query.OrderByDescending ( e => e.Date ).ToListAsync ( );
        }

        public async Task<Expense?> GetExpenseByIdAsync ( long id )
        {
            return await _context.Expenses.Include ( e => e.Category ).FirstOrDefaultAsync ( e => e.ExpenseId == id );
        }

        public async Task<Expense> UpdateExpenseAsync ( Expense expense )
        {
            _context.Expenses.Update ( expense );
            await _context.SaveChangesAsync ( );
            return expense;
        }

        public async Task<Expense?> DeleteExpenseAsync ( long id )
        {
            var expense = await _context.Expenses.FindAsync ( id );
            if ( expense != null )
            {
                _context.Expenses.Remove ( expense );
                await _context.SaveChangesAsync ( );
                return expense;
            }
            return null;
        }
    }
}
