using Core.Enums;

namespace Applications.Dtos.CategoryDtos
{
    public class CategoryResponseDto
    {
        public long CategoryId { get; set; }
        public required string Name { get; set; }
        public string Icon { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public CategoryType Type { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsFavorite { get; set; }
        public bool IsActive { get; set; }
        public DateOnly CreationDate { get; set; }
    }
}
