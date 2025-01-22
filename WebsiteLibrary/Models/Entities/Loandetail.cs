using System;
using System.Collections.Generic;

namespace WebsiteLibrary.Models.Entites;

public partial class Loandetail
{
    public int LoanDetailId { get; set; }

    public int LoanId { get; set; }

    public int BookId { get; set; }

    public int Quantity { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Loan Loan { get; set; } = null!;
}
