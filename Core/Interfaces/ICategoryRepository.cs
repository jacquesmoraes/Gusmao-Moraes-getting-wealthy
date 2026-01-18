using Core.Entities;

namespace Core.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> CreateCategoryAsync ( Category category );
        Task<IEnumerable<Category>> GetAllCategoriesAsync ( );
        Task<Category?> GetCategoryByIdAsync ( long id );
        Task<Category> UpdateCategoryAsync ( Category category );
        Task<Category?> DeleteCategoryAsync ( long id );
    }
}
