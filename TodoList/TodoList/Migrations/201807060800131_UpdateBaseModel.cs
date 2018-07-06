namespace TodoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBaseModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Todos", "CategoryID", "dbo.Categories");
            DropPrimaryKey("dbo.Categories");
            DropPrimaryKey("dbo.Todos");
            AddColumn("dbo.Categories", "CreatedAt", c => c.DateTime(nullable: false, defaultValueSql: "getdate()"));
            AddColumn("dbo.Categories", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Categories", "DeletedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Todos", "CreatedAt", c => c.DateTime(nullable: false, defaultValueSql: "getdate()"));
            AddColumn("dbo.Todos", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Todos", "DeletedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Categories", "ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Todos", "ID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Categories", "ID");
            AddPrimaryKey("dbo.Todos", "ID");
            AddForeignKey("dbo.Todos", "CategoryID", "dbo.Categories", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Todos", "CategoryID", "dbo.Categories");
            DropPrimaryKey("dbo.Todos");
            DropPrimaryKey("dbo.Categories");
            AlterColumn("dbo.Todos", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Categories", "ID", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Todos", "DeletedAt");
            DropColumn("dbo.Todos", "Deleted");
            DropColumn("dbo.Todos", "CreatedAt");
            DropColumn("dbo.Categories", "DeletedAt");
            DropColumn("dbo.Categories", "Deleted");
            DropColumn("dbo.Categories", "CreatedAt");
            AddPrimaryKey("dbo.Todos", "ID");
            AddPrimaryKey("dbo.Categories", "ID");
            AddForeignKey("dbo.Todos", "CategoryID", "dbo.Categories", "ID", cascadeDelete: true);
        }
    }
}
