namespace SnippetManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Snippets",
                c => new
                    {
                        SnippetId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CategoryId = c.Int(),
                        Title = c.String(),
                        Description = c.String(),
                        Language = c.String(),
                        Tags = c.String(),
                        Content = c.String(),
                        IsSynced = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SnippetId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CategoryId)
                .Index(t => t.Title)
                .Index(t => t.Language);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Email = c.String(),
                        PasswordHash = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .Index(t => t.Email, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Snippets", "UserId", "dbo.Users");
            DropForeignKey("dbo.Snippets", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.Snippets", new[] { "Language" });
            DropIndex("dbo.Snippets", new[] { "Title" });
            DropIndex("dbo.Snippets", new[] { "CategoryId" });
            DropIndex("dbo.Snippets", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Snippets");
            DropTable("dbo.Categories");
        }
    }
}
