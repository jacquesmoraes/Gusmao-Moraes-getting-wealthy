using Core.Entities;

namespace Core.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> CreateCategory ( Category category );
       Task<IEnumerable<Category>> GetAllCategories();
        Task<Category?> GetCategoryById ( long id );
        Task<Category?> UpdateCategory ( long id, Category category );
        Task<Category?> DeleteCategory ( long id );

    }
}
