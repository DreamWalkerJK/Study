namespace APIRESTStudy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bookChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "PublishDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Books", "dateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "dateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Books", "PublishDate");
        }
    }
}
