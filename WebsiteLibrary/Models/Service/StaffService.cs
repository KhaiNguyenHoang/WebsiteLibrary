using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebsiteLibrary.Models.Entites;
using WebsiteLibrary.Models.Interface;

namespace WebsiteLibrary.Models.Service
{
    public class StaffService : IStaffService
    {
        private readonly LibraryDatabaseContext _context;

        public StaffService(LibraryDatabaseContext context)
        {
            _context = context;
        }

        public async Task AddStaffAsync(Staff staff)
        {
            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStaffAsync(int staffId, Staff updatedStaff)
        {
            var staff = await _context.Staff.FindAsync(staffId);
            if (staff == null)
                throw new KeyNotFoundException("Staff not found");

            staff.FullName = updatedStaff.FullName;
            staff.Email = updatedStaff.Email;
            staff.PhoneNumber = updatedStaff.PhoneNumber;
            staff.Role = updatedStaff.Role;
            staff.Status = updatedStaff.Status;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteStaffAsync(int staffId)
        {
            var staff = await _context.Staff.FindAsync(staffId);
            if (staff == null)
                throw new KeyNotFoundException("Staff not found");

            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();
        }

        public async Task<Staff> GetStaffByIdAsync(int staffId)
        {
            var staff = await _context.Staff.FindAsync(staffId);
            if (staff == null)
                throw new KeyNotFoundException("Staff not found");

            return staff;
        }

        public async Task<IEnumerable<Staff>> SearchStaffAsync(string keyword)
        {
            return await _context.Staff
                .Where(s => s.FullName.Contains(keyword) || s.Email.Contains(keyword))
                .ToListAsync();
        }

        public async Task UpdateRoleAsync(int staffId, string role)
        {
            var staff = await _context.Staff.FindAsync(staffId);
            if (staff == null)
                throw new KeyNotFoundException("Staff not found");

            staff.Role = role;
            await _context.SaveChangesAsync();
        }
    }
}
