namespace TodoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCreatedAtBaseModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Todos", "CategoryID", "dbo.Categories");
            DropPrimaryKey("dbo.Categories");
            DropPrimaryKey("dbo.Todos");
            AlterColumn("dbo.Categories", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Categories", "CreatedAt", c => c.DateTime(nullable: false, defaultValueSql: "getdate()"));
            AlterColumn("dbo.Todos", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Todos", "CreatedAt", c => c.DateTime(nullable: false, defaultValueSql: "getdate()"));
            AddPrimaryKey("dbo.Categories", "ID");
            AddPrimaryKey("dbo.Todos", "ID");
            AddForeignKey("dbo.Todos", "CategoryID", "dbo.Categories", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Todos", "CategoryID", "dbo.Categories");
            DropPrimaryKey("dbo.Todos");
            DropPrimaryKey("dbo.Categories");
            AlterColumn("dbo.Todos", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Todos", "ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Categories", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Categories", "ID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Todos", "ID");
            AddPrimaryKey("dbo.Categories", "ID");
            AddForeignKey("dbo.Todos", "CategoryID", "dbo.Categories", "ID", cascadeDelete: true);
        }
    }
}
