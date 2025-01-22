using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebsiteLibrary.Models.Entites;
using WebsiteLibrary.Models.Interface;

namespace WebsiteLibrary.Models.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly LibraryDatabaseContext _context;

        public CategoryService(LibraryDatabaseContext context)
        {
            _context = context;
        }

        public async Task AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(int categoryId, Category updatedCategory)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
                throw new KeyNotFoundException("Category not found");

            category.Name = updatedCategory.Name;
            category.Description = updatedCategory.Description;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
                throw new KeyNotFoundException("Category not found");

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByCategoryAsync(int categoryId)
        {
            return await _context.Categories
                .Where(c => c.CategoryId == categoryId)
                .SelectMany(c => c.Books)
                .ToListAsync();
        }
    }
}