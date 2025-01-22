using System;
using System.Collections.Generic;

namespace WebsiteLibrary.Models.Entites;

public partial class Loan
{
    public int LoanId { get; set; }

    public int MemberId { get; set; }

    public int StaffId { get; set; }

    public DateOnly? LoanDate { get; set; }

    public DateOnly DueDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public string? Status { get; set; }

    public string? Remarks { get; set; }

    public virtual ICollection<Fine> Fines { get; set; } = new List<Fine>();

    public virtual ICollection<Loandetail> Loandetails { get; set; } = new List<Loandetail>();

    public virtual Member Member { get; set; } = null!;

    public virtual Staff Staff { get; set; } = null!;
}
