namespace WebsiteLibrary.Models.Interface;

using System.Threading.Tasks;
using WebsiteLibrary.Models.Entites;
public interface IUserService
{
    Task CreateUserAsync(User user); // Tạo tài khoản người dùng
    Task UpdateUserAsync(int userId, User updatedUser); // Cập nhật thông tin người dùng
    Task DeleteUserAsync(int userId); // Xóa tài khoản người dùng
    Task<User> AuthenticateAsync(string username, string password); // Xác thực người dùng
    Task ChangePasswordAsync(int userId, string newPassword); // Đổi mật khẩu
}
