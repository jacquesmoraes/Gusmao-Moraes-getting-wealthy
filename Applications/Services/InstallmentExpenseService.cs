using Core.Entities;
using Core.Enums;
using Core.Interfaces;

namespace Applications.Services
{
    public class InstallmentExpenseService (
        IInstallmentExpenseRepository installmentExpenseRepository,
        ICategoryRepository categoryRepository ) : IInstallmentExpenseService
    {
        private readonly IInstallmentExpenseRepository _installmentExpenseRepository = installmentExpenseRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;

        public async Task<InstallmentExpense> CreateInstallmentExpense ( InstallmentExpense installmentExpense )
        {
            await PrepareInstallmentExpenseAsync ( installmentExpense, recalcItems: true );
            return await _installmentExpenseRepository.CreateInstallmentExpenseAsync ( installmentExpense );
        }

        public async Task<IEnumerable<InstallmentExpense>> GetAllInstallmentExpenses ( )
        {
            return await _installmentExpenseRepository.GetAllInstallmentExpensesAsync ( );
        }

        public async Task<InstallmentExpense?> GetInstallmentExpenseById ( long id )
        {
            return await _installmentExpenseRepository.GetInstallmentExpenseByIdAsync ( id );
        }

        public async Task<InstallmentExpense> UpdateInstallmentExpense ( InstallmentExpense installmentExpense )
        {
            var existing = await _installmentExpenseRepository.GetInstallmentExpenseByIdAsync ( installmentExpense.InstallmentExpenseId );
            if ( existing == null )
            {
                throw new KeyNotFoundException ( "Installment expense not found." );
            }

            var previousNumber = existing.NumberOfInstallments;
            var previousFirstDate = existing.FirstInstallmentDate;
            var previousAmount = existing.Expense?.Amount ?? 0m;
            var hasPaidItems = existing.Items.Any ( item => item.IsPaid );

            ApplyUpdates ( existing, installmentExpense );

            var scheduleChanged =
                previousNumber != existing.NumberOfInstallments ||
                previousFirstDate != existing.FirstInstallmentDate ||
                previousAmount != ( existing.Expense?.Amount ?? 0m );

            if ( hasPaidItems && scheduleChanged )
            {
                throw new ArgumentException ( "Paid installments cannot be rescheduled." );
            }

            await PrepareInstallmentExpenseAsync ( existing, recalcItems: !hasPaidItems );
            return await _installmentExpenseRepository.UpdateInstallmentExpenseAsync ( existing );
        }

        public async Task<InstallmentExpense?> DeleteInstallmentExpense ( long id )
        {
            return await _installmentExpenseRepository.DeleteInstallmentExpenseAsync ( id );
        }

        private static void ApplyUpdates ( InstallmentExpense existing, InstallmentExpense incoming )
        {
            existing.NumberOfInstallments = incoming.NumberOfInstallments;
            existing.FirstInstallmentDate = incoming.FirstInstallmentDate;

            if ( incoming.Expense == null )
            {
                return;
            }

            existing.Expense ??= new Expense ( );
            existing.Expense.Description = incoming.Expense.Description;
            existing.Expense.Amount = incoming.Expense.Amount;
            existing.Expense.Date = incoming.Expense.Date;
            existing.Expense.ExpensePaymentMethod = incoming.Expense.ExpensePaymentMethod;
            existing.Expense.Observations = incoming.Expense.Observations;
            existing.Expense.CategoryId = incoming.Expense.CategoryId;
            existing.Expense.RecurringExpenseId = null;
        }

        private async Task PrepareInstallmentExpenseAsync ( InstallmentExpense installmentExpense, bool recalcItems )
        {
            if ( installmentExpense.NumberOfInstallments <= 0 )
            {
                throw new ArgumentException ( "NumberOfInstallments must be greater than zero." );
            }

            if ( installmentExpense.FirstInstallmentDate == default )
            {
                throw new ArgumentException ( "FirstInstallmentDate is required." );
            }

            if ( installmentExpense.Expense == null )
            {
                throw new ArgumentException ( "Expense data is required." );
            }

            var expense = installmentExpense.Expense;

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

            expense.RecurringExpenseId = null;
            expense.Date = installmentExpense.FirstInstallmentDate;

            if ( expense.CreationDate == default )
            {
                expense.CreationDate = DateOnly.FromDateTime ( DateTime.UtcNow );
            }

            if ( recalcItems )
            {
                BuildInstallmentItems ( installmentExpense, expense.Amount );
            }
        }

        private static void BuildInstallmentItems ( InstallmentExpense installmentExpense, decimal totalAmount )
        {
            var baseAmount = Math.Round (
                totalAmount / installmentExpense.NumberOfInstallments,
                2,
                MidpointRounding.AwayFromZero );

            if ( baseAmount <= 0 )
            {
                throw new ArgumentException ( "Installment amount must be greater than zero." );
            }

            installmentExpense.InstallmentAmount = baseAmount;
            installmentExpense.Items ??= new List<InstallmentExpenseItem> ( );
            installmentExpense.Items.Clear ( );

            var accumulated = 0m;
            for ( var i = 1; i <= installmentExpense.NumberOfInstallments; i++ )
            {
                var dueDate = installmentExpense.FirstInstallmentDate.AddMonths ( i - 1 );
                var amount = i == installmentExpense.NumberOfInstallments
                    ? totalAmount - accumulated
                    : baseAmount;

                installmentExpense.Items.Add ( new InstallmentExpenseItem
                {
                    InstallmentNumber = i,
                    Value = amount,
                    DueDate = dueDate,
                    PaymentDate = null,
                    IsPaid = false,
                    MonthReference = $"{dueDate:yyyy-MM}",
                } );

                accumulated += amount;
            }
        }
    }
}
