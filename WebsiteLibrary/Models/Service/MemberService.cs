using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebsiteLibrary.Models.Entites;
using WebsiteLibrary.Models.Interface;

namespace WebsiteLibrary.Models.Service
{
    public class MemberService : IMemberService
    {
        private readonly LibraryDatabaseContext _context;

        public MemberService(LibraryDatabaseContext context)
        {
            _context = context;
        }

        public async Task AddMemberAsync(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMemberAsync(int memberId, Member updatedMember)
        {
            var member = await _context.Members.FindAsync(memberId);
            if (member == null)
                throw new KeyNotFoundException("Member not found");

            member.FullName = updatedMember.FullName;
            member.Email = updatedMember.Email;
            member.PhoneNumber = updatedMember.PhoneNumber;
            member.Address = updatedMember.Address;
            member.Status = updatedMember.Status;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteMemberAsync(int memberId)
        {
            var member = await _context.Members.FindAsync(memberId);
            if (member == null)
                throw new KeyNotFoundException("Member not found");

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
        }

        public async Task<Member> GetMemberByIdAsync(int memberId)
        {
            var member = await _context.Members.FindAsync(memberId);
            if (member == null)
                throw new KeyNotFoundException("Member not found");

            return member;
        }

        public async Task<IEnumerable<Member>> SearchMembersAsync(string keyword)
        {
            return await _context.Members
                .Where(m => m.FullName.Contains(keyword) || m.Email.Contains(keyword))
                .ToListAsync();
        }

        public async Task UpdateMembershipStatusAsync(int memberId, string status)
        {
            var member = await _context.Members.FindAsync(memberId);
            if (member == null)
                throw new KeyNotFoundException("Member not found");

            member.Status = status;
            await _context.SaveChangesAsync();
        }
    }
}
