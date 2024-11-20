using System;
using System.Collections.Generic;

namespace SnippetManager.Models
{
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

    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<SnippetTag> SnippetTags { get; set; }
    }

    public class SnippetTag
    {
        public int SnippetTagId { get; set; }
        public int SnippetId { get; set; }
        public int TagId { get; set; }
        public virtual Snippet Snippet { get; set; }
        public virtual Tag Tag { get; set; }
    }

    public class CloudSync
    {
        public int SyncId { get; set; } // This is the primary key
        public int UserId { get; set; }
        public int SnippetId { get; set; }
        public DateTime SyncedAt { get; set; }
        public string Status { get; set; }
        public virtual User User { get; set; }
        public virtual Snippet Snippet { get; set; }
    }
}