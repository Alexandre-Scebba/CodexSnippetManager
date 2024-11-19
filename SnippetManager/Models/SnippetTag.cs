namespace SnippetManager.Models;
public class SnippetTag
{
    public int SnippetTagId { get; set; }
    public int SnippetId { get; set; }
    public int TagId { get; set; }

    public virtual Snippet Snippet { get; set; }
    public virtual Tag Tag { get; set; }
}