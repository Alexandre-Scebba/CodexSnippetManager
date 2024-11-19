using System;
using System.Collections.Generic;

namespace SnippetManager.Models;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Snippet> Snippets { get; set; }
    public virtual ICollection<CloudSync> CloudSyncs { get; set; }
}