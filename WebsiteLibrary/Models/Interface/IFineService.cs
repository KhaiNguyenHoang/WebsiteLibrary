namespace WebsiteLibrary.Models.Interface;

using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteLibrary.Models.Entites;
public interface IFineService
{
    Task AddFineAsync(Fine fine); // Thêm giao dịch phạt
    Task UpdateFineStatusAsync(int fineId, string status); // Cập nhật trạng thái tiền phạt (Paid/Unpaid)
    Task<IEnumerable<Fine>> GetFinesByMemberIdAsync(int memberId); // Lấy danh sách tiền phạt theo thành viên
    Task<IEnumerable<Fine>> GetUnpaidFinesAsync(); // Lấy danh sách các tiền phạt chưa thanh toán
}
