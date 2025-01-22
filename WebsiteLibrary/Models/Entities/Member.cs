using System;
using System.Collections.Generic;

namespace WebsiteLibrary.Models.Entites;

public partial class Member
{
    public int MemberId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public DateOnly? MembershipDate { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
