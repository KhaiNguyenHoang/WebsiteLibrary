using System;
using System.Collections.Generic;

namespace WebsiteLibrary.Models.Entites;

public partial class Staff
{
    public int StaffId { get; set; }

    public string FullName { get; set; } = null!;

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Role { get; set; }

    public DateOnly? HireDate { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Bookimport> Bookimports { get; set; } = new List<Bookimport>();

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
