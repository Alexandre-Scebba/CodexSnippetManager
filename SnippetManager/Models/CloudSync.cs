using System;
namespace SnippetManager.Models;
public class CloudSync
{
    public int SyncId { get; set; }
    public int UserId { get; set; }
    public int SnippetId { get; set; }
    public DateTime SyncedAt { get; set; }
    public string Status { get; set; } = string.Empty;

    public virtual User User { get; set; } = new User();
    public virtual Snippet Snippet { get; set; } = new Snippet();
}