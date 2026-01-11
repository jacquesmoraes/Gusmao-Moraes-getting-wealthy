using Core.Enums;

namespace Core.Entities
{
    public class Category
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public string Icon { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public CategoryType Type { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsFavorite { get; set; }
        public bool IsActive { get; set; }
        public DateOnly CriationDate { get; set; }

        public ICollection<Expense> Expenses { get; set; } = [];
        public ICollection<Income> Incomes { get; set; } = [];


    }
}
