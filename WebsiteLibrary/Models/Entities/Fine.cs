using System;
using System.Collections.Generic;

namespace WebsiteLibrary.Models.Entites;

public partial class Fine
{
    public int FineId { get; set; }

    public int LoanId { get; set; }

    public decimal FineAmount { get; set; }

    public DateOnly? PaidDate { get; set; }

    public string? Status { get; set; }

    public virtual Loan Loan { get; set; } = null!;
}
