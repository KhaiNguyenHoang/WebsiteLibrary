using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebsiteLibrary.Models.Entites;
using WebsiteLibrary.Models.Interface;

namespace WebsiteLibrary.Models.Service
{
    public class FineService : IFineService
    {
        private readonly LibraryDatabaseContext _context;

        public FineService(LibraryDatabaseContext context)
        {
            _context = context;
        }

        public async Task AddFineAsync(Fine fine)
        {
            _context.Fines.Add(fine);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFineStatusAsync(int fineId, string status)
        {
            var fine = await _context.Fines.FindAsync(fineId);
            if (fine == null)
                throw new KeyNotFoundException("Fine not found");

            fine.Status = status;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Fine>> GetFinesByMemberIdAsync(int memberId)
        {
            return await _context.Fines
                .Include(f => f.Loan)
                .Where(f => f.Loan.MemberId == memberId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Fine>> GetUnpaidFinesAsync()
        {
            return await _context.Fines
                .Where(f => f.Status == "Unpaid")
                .ToListAsync();
        }
    }
}