using System;
using System.Collections.Generic;

namespace WebsiteLibrary.Models.Entites;

public partial class Bookimportdetail
{
    public int ImportDetailId { get; set; }

    public int ImportId { get; set; }

    public int BookId { get; set; }

    public int Quantity { get; set; }

    public decimal? Price { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Bookimport Import { get; set; } = null!;
}
