using Core.Entities;
using Core.Enums;
using Core.Interfaces;

namespace Applications.Services
{
    public class ExpenseService ( IExpenseRepository expenseRepository, ICategoryRepository categoryRepository ) : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository = expenseRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;


        public async Task<Expense> CreateExpense ( Expense expense )
        {
            if ( expense.CreationDate == default )
            {
                expense.CreationDate = DateOnly.FromDateTime ( DateTime.UtcNow );
            }
            await ValidateExpenseAsync ( expense );
            return await _expenseRepository.CreateExpenseAsync ( expense );
        }


        public async Task<IEnumerable<Expense>> GetAllExpenses(long? categoryId = null, DateOnly? startDate = null, DateOnly? endDate = null)
{
    return await _expenseRepository.GetAllExpensesAsync(categoryId, startDate, endDate);
}

        public async Task<Expense?> GetExpenseById ( long id )
        {
            return await _expenseRepository.GetExpenseByIdAsync ( id );
        }

        public async Task<Expense?> UpdateExpense ( long id, Expense expense )
        {
            var existing = await _expenseRepository.GetExpenseByIdAsync ( id );
            if ( existing == null )
            {
                return null;
            }

            existing.Description = expense.Description;
            existing.Amount = expense.Amount;
            existing.Date = expense.Date;
            existing.ExpensePaymentMethod = expense.ExpensePaymentMethod;
            existing.Observations = expense.Observations;
            existing.CategoryId = expense.CategoryId;
            existing.InstallmentExpenseId = expense.InstallmentExpenseId;
            existing.RecurringExpenseId = expense.RecurringExpenseId;
            await ValidateExpenseAsync ( existing );
            return await _expenseRepository.UpdateExpenseAsync ( existing );

        }
        public async Task<Expense?> DeleteExpense ( long id )
        {
            return await _expenseRepository.DeleteExpenseAsync ( id );
        }

        private async Task ValidateExpenseAsync ( Expense expense )
        {
            if ( string.IsNullOrWhiteSpace ( expense.Description ) )
            {
                throw new ArgumentException ( "Description is required." );
            }

            if ( expense.Description.Length > 200 )
            {
                throw new ArgumentException ( "Description must be 200 characters or less." );
            }

            if ( expense.Amount <= 0 )
            {
                throw new ArgumentException ( "Amount must be greater than zero." );
            }

            if ( expense.Date == default )
            {
                throw new ArgumentException ( "Date is required." );
            }

            if ( !Enum.IsDefined ( expense.ExpensePaymentMethod ) )
            {
                throw new ArgumentException ( "Payment method is invalid." );
            }

            if ( expense.Observations.Length > 200 )
            {
                throw new ArgumentException ( "Observations must be 200 characters or less." );
            }

            if ( expense.CategoryId <= 0 )
            {
                throw new ArgumentException ( "CategoryId must be greater than zero." );
            }

            if ( expense.InstallmentExpenseId.HasValue && expense.InstallmentExpenseId.Value <= 0 )
            {
                throw new ArgumentException ( "InstallmentExpenseId must be greater than zero." );
            }

            if ( expense.RecurringExpenseId.HasValue && expense.RecurringExpenseId.Value <= 0 )
            {
                throw new ArgumentException ( "RecurringExpenseId must be greater than zero." );
            }

            if ( expense.InstallmentExpenseId.HasValue && expense.RecurringExpenseId.HasValue )
            {
                throw new ArgumentException ( "InstallmentExpenseId and RecurringExpenseId cannot both be set." );
            }

            var category = await _categoryRepository.GetCategoryByIdAsync ( expense.CategoryId );
            if ( category == null )
            {
                throw new ArgumentException ( "Category not found." );
            }

            if ( category.Type != CategoryType.Expense )
            {
                throw new ArgumentException ( "Category type must be Expense." );
            }

            if ( !category.IsActive )
            {
                throw new ArgumentException ( "Category is inactive." );
            }
          
        }


    }
}
