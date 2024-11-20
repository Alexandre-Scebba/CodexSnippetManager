using System.Data.Entity;
using SnippetManager.Models;
using Microsoft.Data.SqlClient;

namespace SnippetManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=DefaultConnection")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Snippet> Snippets { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<SnippetTag> SnippetTags { get; set; }
        public DbSet<CloudSync> CloudSyncs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Snippets)
                .WithRequired(s => s.User)
                .HasForeignKey(s => s.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.CloudSyncs)
                .WithRequired(cs => cs.User)
                .HasForeignKey(cs => cs.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Snippet>()
                .HasMany(s => s.SnippetTags)
                .WithRequired(st => st.Snippet)
                .HasForeignKey(st => st.SnippetId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tag>()
                .HasMany(t => t.SnippetTags)
                .WithRequired(st => st.Tag)
                .HasForeignKey(st => st.TagId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CloudSync>()
                .HasRequired(cs => cs.Snippet)
                .WithMany()
                .HasForeignKey(cs => cs.SnippetId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CloudSync>() // CloudSync primary key
                .HasKey(cs => cs.SyncId);
        }


        public bool CanConnect()
        {
            try
            {
                this.Database.Connection.Open();
                this.Database.Connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}