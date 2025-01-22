namespace WebsiteLibrary.Models.Interface;

using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteLibrary.Models.Entites;
public interface IBookService
{
    Task AddBookAsync(Book book); // Thêm sách mới
    Task UpdateBookAsync(int bookId, Book updatedBook); // Cập nhật sách
    Task DeleteBookAsync(int bookId); // Xóa sách
    Task<Book> GetBookByIdAsync(int bookId); // Lấy thông tin sách theo ID
    Task<IEnumerable<Book>> GetAllBooksAsync(); // Lấy danh sách tất cả sách
    Task<IEnumerable<Book>> SearchBooksAsync(string keyword); // Tìm kiếm sách theo từ khóa
    Task UpdateQuantityAsync(int bookId, int quantity); // Cập nhật số lượng sách
}
