namespace WebsiteLibrary.Models.Interface;

using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteLibrary.Models.Entites;
public interface IMemberService
{
    Task AddMemberAsync(Member member); // Thêm thành viên mới
    Task UpdateMemberAsync(int memberId, Member updatedMember); // Cập nhật thông tin thành viên
    Task DeleteMemberAsync(int memberId); // Xóa thành viên
    Task<Member> GetMemberByIdAsync(int memberId); // Lấy thông tin thành viên theo ID
    Task<IEnumerable<Member>> SearchMembersAsync(string keyword); // Tìm kiếm thành viên
    Task UpdateMembershipStatusAsync(int memberId, string status); // Cập nhật trạng thái thành viên (Active/Inactive)
}
