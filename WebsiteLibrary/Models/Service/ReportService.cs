namespace WebsiteLibrary.Models.Service;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebsiteLibrary.Models.Interface;
using Microsoft.EntityFrameworkCore;
using WebsiteLibrary.Models.Entites;

public class ReportService : IReportService
{
    private readonly LibraryDatabaseContext _context;

    public ReportService(LibraryDatabaseContext context)
    {
        _context = context;
    }

    // Get the list of most borrowed books
    public async Task<IEnumerable<Book>> GetMostBorrowedBooksAsync()
    {
        return await _context.Loandetails
            .GroupBy(ld => ld.BookId)
            .OrderByDescending(g => g.Count()) // Count the number of times each book was borrowed
            .Select(g => g.Key) // Select BookId
            .Join(_context.Books, bookId => bookId, book => book.BookId, (bookId, book) => book)
            .Include(b => b.Categories) // Include categories (if necessary)
            .Take(10) // Get top 10 books
            .ToListAsync();
    }

    // Get the list of active members (with the most loan transactions)
    public async Task<IEnumerable<Member>> GetActiveMembersAsync()
    {
        return await _context.Loans
            .GroupBy(l => l.MemberId)
            .OrderByDescending(g => g.Count()) // Count the number of loan transactions for each member
            .Select(g => g.Key) // Select MemberId
            .Join(_context.Members, memberId => memberId, member => member.MemberId, (memberId, member) => member)
            .Take(10) // Get top 10 members
            .ToListAsync();
    }

    public async Task<IEnumerable<Fine>> GetUnpaidFinesAsync()
    {
        // Truy vấn danh sách các khoản phạt chưa thanh toán
        return await _context.Fines
            .Where(f => f.Status == "Unpaid") // Chỉ lấy các khoản phạt chưa thanh toán
            .Include(f => f.Loan) // Bao gồm thông tin giao dịch mượn liên quan
            .ThenInclude(l => l.Member) // Bao gồm thông tin thành viên của giao dịch mượn
            .ToListAsync();
    }
}
