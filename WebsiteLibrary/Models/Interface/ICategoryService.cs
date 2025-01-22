namespace WebsiteLibrary.Models.Interface;

using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteLibrary.Models.Entites;
public interface ICategoryService
{
    Task AddCategoryAsync(Category category); // Thêm danh mục sách
    Task UpdateCategoryAsync(int categoryId, Category updatedCategory); // Cập nhật danh mục sách
    Task DeleteCategoryAsync(int categoryId); // Xóa danh mục sách
    Task<IEnumerable<Category>> GetAllCategoriesAsync(); // Lấy danh sách tất cả danh mục
    Task<IEnumerable<Book>> GetBooksByCategoryAsync(int categoryId); // Lấy danh sách sách theo danh mục
}
