//using System.Data.Entity;
//using Azure;
//using SnippetManager.Models;

//namespace SnippetManager.Data
//{
//    public class ApplicationDbContext : DbContext
//    {
//        public ApplicationDbContext() : base("name=DefaultConnection")
//        {
//        }

//        public DbSet<User> Users { get; set; } = null!;
//        public DbSet<Snippet> Snippets { get; set; } = null!;
//        public DbSet<Category> Categories { get; set; } = null!;



//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);

//            modelBuilder.Entity<User>()
//                .HasMany(u => u.Snippets)
//                .WithRequired(s => s.User)
//                .HasForeignKey(s => s.UserId)
//                .WillCascadeOnDelete(false);

//            modelBuilder.Entity<Snippet>()
//                .HasOptional(s => s.Category)
//                .WithMany(c => c.Snippets)
//                .HasForeignKey(s => s.CategoryId)
//                .WillCascadeOnDelete(false);

//            modelBuilder.Entity<User>()
//                .Property(u => u.CreatedAt)
//                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed);

//            modelBuilder.Entity<User>()
//                .Property(u => u.UpdatedAt)
//                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed);

//            modelBuilder.Entity<Snippet>()
//                .Property(s => s.CreatedAt)
//                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed);

//            modelBuilder.Entity<Snippet>()
//                .Property(s => s.UpdatedAt)
//                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Computed);

//            modelBuilder.Entity<User>()
//                .HasIndex(u => u.Email)
//                .IsUnique();

//            modelBuilder.Entity<Snippet>()
//                .HasIndex(s => s.Title);

//            modelBuilder.Entity<Snippet>()
//                .HasIndex(s => s.Language);

//            //added 
//            modelBuilder.Entity<Snippet>()
//                .HasIndex(s => s.Tags);

//            modelBuilder.Entity<Snippet>()
//                .HasIndex(s => s.Language);
//        }

//        public bool CanConnect()
//        {
//            try
//            {
//                this.Database.Connection.Open();
//                this.Database.Connection.Close();
//                return true;
//            }
//            catch
//            {
//                return false;
//            }
//        }
//    }
//}
