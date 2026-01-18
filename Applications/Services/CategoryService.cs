using Core.Entities;
using Core.Interfaces;

namespace Applications.Services
{
    public class CategoryService ( ICategoryRepository categoryRepository ) : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;

        public async Task<Category> CreateCategory ( Category category )
        {
            if ( category.CreationDate == default )
            {
                category.CreationDate = DateOnly.FromDateTime ( DateTime.UtcNow );
            }
            return await _categoryRepository.CreateCategoryAsync ( category );
        }

        public async Task<IEnumerable<Category>> GetAllCategories ( )
        {
            return await _categoryRepository.GetAllCategoriesAsync ( );
        }

        public async Task<Category?> GetCategoryById ( long id )
        {
            return await _categoryRepository.GetCategoryByIdAsync ( id );
        }

        public async Task<Category?> UpdateCategory ( long id, Category category )
        {
            var existing = await _categoryRepository.GetCategoryByIdAsync ( id );
            if ( existing == null )
            {
                return null;
            }

            existing.Name = category.Name;
            existing.Icon = category.Icon;
            existing.Color = category.Color;
            existing.Type = category.Type;
            existing.Description = category.Description;
            existing.IsFavorite = category.IsFavorite;
            existing.IsActive = category.IsActive;

            return await _categoryRepository.UpdateCategoryAsync ( existing );
        }

        public async Task<Category?> DeleteCategory ( long id )
        {
            return await _categoryRepository.DeleteCategoryAsync ( id );
        }
    }
}
