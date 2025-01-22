using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebsiteLibrary.Models.Entites;
using WebsiteLibrary.Models.Interface;

namespace WebsiteLibrary.Models.Service
{
    public class LoanService : ILoanService
    {
        private readonly LibraryDatabaseContext _context;

        public LoanService(LibraryDatabaseContext context)
        {
            _context = context;
        }

        public async Task CreateLoanAsync(Loan loan)
        {
            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();
        }

        public async Task ReturnBookAsync(int loanId, DateTime returnDate)
        {
            var loan = await _context.Loans.FindAsync(loanId);
            if (loan == null)
                throw new KeyNotFoundException("Loan not found");

            loan.ReturnDate = returnDate;
            loan.Status = "Returned";
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Loan>> GetLoansByMemberIdAsync(int memberId)
        {
            return await _context.Loans
                .Include(l => l.Loandetails)
                .ThenInclude(ld => ld.Book)
                .Where(l => l.MemberId == memberId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Loan>> GetLoansByStatusAsync(string status)
        {
            return await _context.Loans
                .Include(l => l.Loandetails)
                .ThenInclude(ld => ld.Book)
                .Where(l => l.Status == status)
                .ToListAsync();
        }

        public async Task MarkLoanAsOverdueAsync(int loanId)
        {
            var loan = await _context.Loans.FindAsync(loanId);
            if (loan == null)
                throw new KeyNotFoundException("Loan not found");

            loan.Status = "Overdue";
            await _context.SaveChangesAsync();
        }
    }
}
