using Core.Entities;
using Core.Enums;
using Core.Interfaces;

namespace Applications.Services
{
    public class RecurringExpenseService (
        IRecurringExpenseRepository recurringExpenseRepository,
        ICategoryRepository categoryRepository ) : IRecurringExpenseService
    {
        private readonly IRecurringExpenseRepository _recurringExpenseRepository = recurringExpenseRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;

        public async Task<RecurringExpense> CreateRecurringExpense ( RecurringExpense recurringExpense )
        {
            await PrepareRecurringExpenseAsync ( recurringExpense );
            return await _recurringExpenseRepository.CreateRecurringExpenseAsync ( recurringExpense );
        }

        public async Task<IEnumerable<RecurringExpense>> GetAllRecurringExpenses ( )
        {
            return await _recurringExpenseRepository.GetAllRecurringExpensesAsync ( );
        }

        public async Task<RecurringExpense?> GetRecurringExpenseById ( long id )
        {
            return await _recurringExpenseRepository.GetRecurringExpenseByIdAsync ( id );
        }

        public async Task<IEnumerable<RecurringExpense>> GetActiveRecurringExpensesDue ( DateOnly date )
        {
            return await _recurringExpenseRepository.GetActiveRecurringExpensesDueAsync ( date );
        }

        public async Task<RecurringExpense> UpdateRecurringExpense ( RecurringExpense recurringExpense )
        {
            var existing = await _recurringExpenseRepository.GetRecurringExpenseByIdAsync ( recurringExpense.RecurringExpenseId );
            if ( existing == null )
            {
                throw new KeyNotFoundException ( "Recurring expense not found." );
            }

            ApplyUpdates ( existing, recurringExpense );
            await PrepareRecurringExpenseAsync ( existing );
            return await _recurringExpenseRepository.UpdateRecurringExpenseAsync ( existing );
        }

        public async Task<RecurringExpense?> DeleteRecurringExpense ( long id )
        {
            return await _recurringExpenseRepository.DeleteRecurringExpenseAsync ( id );
        }

        public async Task<RecurringExpense?> ToggleActive ( long id )
        {
            var existing = await _recurringExpenseRepository.GetRecurringExpenseByIdAsync ( id );
            if ( existing == null )
            {
                return null;
            }

            existing.IsActive = !existing.IsActive;
            return await _recurringExpenseRepository.UpdateRecurringExpenseAsync ( existing );
        }

        private static void ApplyUpdates ( RecurringExpense existing, RecurringExpense incoming )
        {
            existing.RecurringFrequency = incoming.RecurringFrequency;
            existing.Day = incoming.Day;
            existing.StartDate = incoming.StartDate;
            existing.EndDate = incoming.EndDate;
            existing.IsActive = incoming.IsActive;

            if ( incoming.Expense == null )
            {
                return;
            }

            existing.Expense ??= new Expense ( );
            existing.Expense.Description = incoming.Expense.Description;
            existing.Expense.Amount = incoming.Expense.Amount;
            existing.Expense.ExpensePaymentMethod = incoming.Expense.ExpensePaymentMethod;
            existing.Expense.Observations = incoming.Expense.Observations;
            existing.Expense.CategoryId = incoming.Expense.CategoryId;
            existing.Expense.InstallmentExpenseId = null;
        }

        private async Task PrepareRecurringExpenseAsync ( RecurringExpense recurringExpense )
        {
            if ( !Enum.IsDefined ( recurringExpense.RecurringFrequency ) )
            {
                throw new ArgumentException ( "Recurring frequency is invalid." );
            }

            if ( recurringExpense.Day <= 0 )
            {
                throw new ArgumentException ( "Day must be greater than zero." );
            }

            if ( recurringExpense.StartDate == default )
            {
                throw new ArgumentException ( "StartDate is required." );
            }

            if ( recurringExpense.EndDate.HasValue && recurringExpense.EndDate.Value < recurringExpense.StartDate )
            {
                throw new ArgumentException ( "EndDate must be on or after StartDate." );
            }

            ValidateDayByFrequency ( recurringExpense.RecurringFrequency, recurringExpense.Day );

            if ( recurringExpense.Expense == null )
            {
                throw new ArgumentException ( "Expense template is required." );
            }

            var expense = recurringExpense.Expense;

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

            expense.InstallmentExpenseId = null;
            expense.Date = recurringExpense.StartDate;

            if ( expense.CreationDate == default )
            {
                expense.CreationDate = DateOnly.FromDateTime ( DateTime.UtcNow );
            }

            if ( recurringExpense.CreationDate == default )
            {
                recurringExpense.CreationDate = DateOnly.FromDateTime ( DateTime.UtcNow );
            }

            recurringExpense.NextDate = CalculateNextDate ( recurringExpense.StartDate, recurringExpense.RecurringFrequency, recurringExpense.Day );

            if ( recurringExpense.EndDate.HasValue && recurringExpense.NextDate > recurringExpense.EndDate.Value )
            {
                throw new ArgumentException ( "NextDate must be on or before EndDate." );
            }
        }

        private static void ValidateDayByFrequency ( Frequency frequency, int day )
        {
            if ( frequency == Frequency.Weekly || frequency == Frequency.Biweekly )
            {
                if ( day < 1 || day > 7 )
                {
                    throw new ArgumentException ( "Day must be between 1 and 7 for weekly frequencies." );
                }
                return;
            }

            if ( day < 1 || day > 31 )
            {
                throw new ArgumentException ( "Day must be between 1 and 31 for monthly frequencies." );
            }
        }

        private static DateOnly CalculateNextDate ( DateOnly startDate, Frequency frequency, int day )
        {
            return frequency switch
            {
                Frequency.Weekly => NextWeeklyDate ( startDate, day ),
                Frequency.Biweekly => NextWeeklyDate ( startDate, day ),
                Frequency.Monthly => NextMonthlyDate ( startDate, 1, day ),
                Frequency.Quarterly => NextMonthlyDate ( startDate, 3, day ),
                Frequency.Semiannual => NextMonthlyDate ( startDate, 6, day ),
                Frequency.Annual => NextMonthlyDate ( startDate, 12, day ),
                _ => startDate
            };
        }

        private static DateOnly NextWeeklyDate ( DateOnly startDate, int day )
        {
            var currentDow = ( int ) startDate.DayOfWeek;
            var targetDow = day % 7;
            if ( targetDow == 0 )
            {
                targetDow = 7;
            }

            var startDow = currentDow == 0 ? 7 : currentDow;
            var delta = targetDow - startDow;
            if ( delta < 0 )
            {
                delta += 7;
            }

            return startDate.AddDays ( delta );
        }

        private static DateOnly NextMonthlyDate ( DateOnly startDate, int monthStep, int day )
        {

            var next = startDate.AddMonths ( monthStep );
            var candidate = BuildSafeDate ( next.Year, next.Month, day );


            if ( candidate <= startDate )
            {
                next = next.AddMonths ( monthStep );
                candidate = BuildSafeDate ( next.Year, next.Month, day );
            }

            return candidate;
        }

        private static DateOnly BuildSafeDate ( int year, int month, int day )
        {
            var maxDay = DateTime.DaysInMonth ( year, month );
            var resolvedDay = Math.Min ( day, maxDay );
            return new DateOnly ( year, month, resolvedDay );
        }
    }
}
