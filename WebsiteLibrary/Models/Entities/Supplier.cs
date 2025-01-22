using System;
using System.Collections.Generic;

namespace WebsiteLibrary.Models.Entites;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string Name { get; set; } = null!;

    public string? ContactName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Bookimport> Bookimports { get; set; } = new List<Bookimport>();
}
