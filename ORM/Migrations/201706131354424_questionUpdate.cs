namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class questionUpdate : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Questions", name: "Test_Id", newName: "TestId");
            RenameIndex(table: "dbo.Questions", name: "IX_Test_Id", newName: "IX_TestId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Questions", name: "IX_TestId", newName: "IX_Test_Id");
            RenameColumn(table: "dbo.Questions", name: "TestId", newName: "Test_Id");
        }
    }
}
