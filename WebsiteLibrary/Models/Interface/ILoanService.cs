namespace WebsiteLibrary.Models.Interface;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteLibrary.Models.Entites;
public interface ILoanService
{
    Task CreateLoanAsync(Loan loan); // Tạo giao dịch mượn sách
    Task ReturnBookAsync(int loanId, DateTime returnDate); // Xác nhận trả sách
    Task<IEnumerable<Loan>> GetLoansByMemberIdAsync(int memberId); // Lấy danh sách mượn theo thành viên
    Task<IEnumerable<Loan>> GetLoansByStatusAsync(string status); // Lấy danh sách mượn theo trạng thái
    Task MarkLoanAsOverdueAsync(int loanId); // Đánh dấu giao dịch mượn sách quá hạn
}
