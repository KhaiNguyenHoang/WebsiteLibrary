namespace WebsiteLibrary.Models.Interface;

using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteLibrary.Models.Entites;
public interface IStaffService
{
    Task AddStaffAsync(Staff staff); // Thêm nhân viên mới
    Task UpdateStaffAsync(int staffId, Staff updatedStaff); // Cập nhật thông tin nhân viên
    Task DeleteStaffAsync(int staffId); // Xóa nhân viên
    Task<Staff> GetStaffByIdAsync(int staffId); // Lấy thông tin nhân viên theo ID
    Task<IEnumerable<Staff>> SearchStaffAsync(string keyword); // Tìm kiếm nhân viên
    Task UpdateRoleAsync(int staffId, string role); // Cập nhật vai trò nhân viên (Librarian/Assistant)
}
