using System;
using System.Collections.Generic;

namespace WebsiteLibrary.Models.Entites;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public string? Publisher { get; set; }

    public short? PublishedYear { get; set; }

    public string? Isbn { get; set; }

    public string? Genre { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Bookimportdetail> Bookimportdetails { get; set; } = new List<Bookimportdetail>();

    public virtual ICollection<Loandetail> Loandetails { get; set; } = new List<Loandetail>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
