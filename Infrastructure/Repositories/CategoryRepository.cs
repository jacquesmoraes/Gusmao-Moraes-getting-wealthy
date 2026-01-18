using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CategoryRepository ( FinancialContext context ) : ICategoryRepository
    {
        private readonly FinancialContext _context = context;

        public async Task<Category> CreateCategoryAsync ( Category category )
        {
            _context.Categories.Add ( category );
            await _context.SaveChangesAsync ( );
            return category;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync ( )
        {
            return  await _context.Categories.ToListAsync ( );
         

        }

        public async Task<Category?> GetCategoryByIdAsync ( long id )
        {
            return await _context.Categories.FindAsync ( id );
        }

        public async Task<Category> UpdateCategoryAsync ( Category category )
        {
            _context.Categories.Update ( category );
            await _context.SaveChangesAsync ( );
            return category;
        }

        public async Task<Category?> DeleteCategoryAsync ( long id )
        {
            var category = await _context.Categories.FindAsync ( id );
            if ( category == null )
            {
                return null;
            }

            _context.Categories.Remove ( category );
            await _context.SaveChangesAsync ( );
            return category;
        }
    }
}
