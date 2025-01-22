using System;
using System.Collections.Generic;

namespace WebsiteLibrary.Models.Entites;

public partial class Bookimport
{
    public int ImportId { get; set; }

    public int SupplierId { get; set; }

    public int StaffId { get; set; }

    public DateOnly? ImportDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public virtual ICollection<Bookimportdetail> Bookimportdetails { get; set; } = new List<Bookimportdetail>();

    public virtual Staff Staff { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}
