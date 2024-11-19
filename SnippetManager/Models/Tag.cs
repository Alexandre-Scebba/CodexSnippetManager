using System.Collections.Generic;
namespace SnippetManager.Models;
public class Tag
{
    public int TagId { get; set; }
    public string Name { get; set; }

    public virtual ICollection<SnippetTag> SnippetTags { get; set; }
}