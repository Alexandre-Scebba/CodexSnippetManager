using System;
using System.Collections.Generic;
namespace SnippetManager.Models;
public class Snippet
{
    public int SnippetId { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Language { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public virtual User User { get; set; }
    public virtual ICollection<SnippetTag> SnippetTags { get; set; }
}