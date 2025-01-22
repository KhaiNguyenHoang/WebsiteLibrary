using System;
using System.Collections.Generic;

namespace WebsiteLibrary.Models.Entites;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public int ReferenceId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Member Reference { get; set; } = null!;

    public virtual Staff ReferenceNavigation { get; set; } = null!;
}
