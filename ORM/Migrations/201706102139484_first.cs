namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        IsRight = c.Boolean(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        TimeLimit = c.Time(nullable: false, precision: 7),
                        MinToSuccess = c.Double(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        ThemeId = c.Int(nullable: false),
                        Profile_Id = c.Int(),
                        Profile_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Themes", t => t.ThemeId, cascadeDelete: true)
                .ForeignKey("dbo.Profiles", t => t.Profile_Id)
                .ForeignKey("dbo.Profiles", t => t.Profile_Id1)
                .Index(t => t.ThemeId)
                .Index(t => t.Profile_Id)
                .Index(t => t.Profile_Id1);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ThemeId = c.Int(nullable: false),
                        Text = c.String(),
                        Test_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Themes", t => t.ThemeId, cascadeDelete: true)
                .ForeignKey("dbo.Tests", t => t.Test_Id)
                .Index(t => t.ThemeId)
                .Index(t => t.Test_Id);
            
            CreateTable(
                "dbo.Themes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TestResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TestId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Runtime = c.Time(nullable: false, precision: 7),
                        DateComplete = c.DateTime(nullable: false),
                        IsSuccess = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tests", t => t.TestId, cascadeDelete: true)
                .Index(t => t.TestId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        ProfileId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profiles", t => t.ProfileId)
                .Index(t => t.ProfileId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Role_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Role_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.Tests", "Profile_Id1", "dbo.Profiles");
            DropForeignKey("dbo.Tests", "Profile_Id", "dbo.Profiles");
            DropForeignKey("dbo.Tests", "ThemeId", "dbo.Themes");
            DropForeignKey("dbo.TestResults", "TestId", "dbo.Tests");
            DropForeignKey("dbo.Questions", "Test_Id", "dbo.Tests");
            DropForeignKey("dbo.Questions", "ThemeId", "dbo.Themes");
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropIndex("dbo.UserRoles", new[] { "Role_Id" });
            DropIndex("dbo.UserRoles", new[] { "User_Id" });
            DropIndex("dbo.Users", new[] { "ProfileId" });
            DropIndex("dbo.TestResults", new[] { "TestId" });
            DropIndex("dbo.Questions", new[] { "Test_Id" });
            DropIndex("dbo.Questions", new[] { "ThemeId" });
            DropIndex("dbo.Tests", new[] { "Profile_Id1" });
            DropIndex("dbo.Tests", new[] { "Profile_Id" });
            DropIndex("dbo.Tests", new[] { "ThemeId" });
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.TestResults");
            DropTable("dbo.Themes");
            DropTable("dbo.Questions");
            DropTable("dbo.Tests");
            DropTable("dbo.Profiles");
            DropTable("dbo.Answers");
        }
    }
}
