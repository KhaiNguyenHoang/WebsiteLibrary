namespace WebsiteLibrary.Models.Interface;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteLibrary.Models.Entites;
public interface IReportService
{
    Task<IEnumerable<Book>> GetMostBorrowedBooksAsync(); // Lấy danh sách sách được mượn nhiều nhất
    Task<IEnumerable<Member>> GetActiveMembersAsync(); // Lấy danh sách thành viên tích cực
    Task<IEnumerable<Fine>> GetUnpaidFinesAsync(); // Lấy danh sách phạt chưa thanh toán
    Task<decimal> CalculateRevenueAsync(DateTime startDate, DateTime endDate); // Tính doanh thu trong khoảng thời gian
}