namespace MvcStudy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inheritance : DbMigration
    {
        public override void Up()
        {
            //RenameTable(name: "dbo.Instructor", newName: "Person");
            //AddColumn("dbo.Person", "EnrollmentDate", c => c.DateTime());
            //AddColumn("dbo.Person", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            //AlterColumn("dbo.Person", "Name", c => c.String(nullable: false, maxLength: 50));
            //AlterColumn("dbo.Person", "HireDate", c => c.DateTime());
            //DropTable("dbo.Student");

            // Drop foreign keys and indexes that point to tables we're going to drop.
            DropForeignKey("dbo.Enrollment", "StudentId", "dbo.Student");
            DropIndex("dbo.Enrollment", new[] { "StudentId" });

            RenameTable(name: "dbo.Instructor", newName: "Person");
            AddColumn("dbo.Person", "EnrollmentDate", c => c.DateTime());
            AddColumn("dbo.Person", "Discriminator", c => c.String(nullable: false, maxLength: 128, defaultValue: "Instructor"));
            AlterColumn("dbo.Person", "HireDate", c => c.DateTime());
            AddColumn("dbo.Person", "OldId", c => c.Int(nullable: true));

            // Copy existing Student data into new Person table.
            Sql("INSERT INTO dbo.Person (Name, HireDate, EnrollmentDate, Discriminator, OldId) SELECT Name, null AS HireDate, EnrollmentDate, 'Student' AS Discriminator, ID AS OldId FROM dbo.Student");

            // Fix up existing relationships to match new PK's.
            Sql("UPDATE dbo.Enrollment SET StudentId = (SELECT ID FROM dbo.Person WHERE OldId = Enrollment.StudentId AND Discriminator = 'Student')");

            // Remove temporary key
            DropColumn("dbo.Person", "OldId");

            DropTable("dbo.Student");

            // Re-create foreign keys and indexes pointing to new table.
            AddForeignKey("dbo.Enrollment", "StudentId", "dbo.Person", "Id", cascadeDelete: true);
            CreateIndex("dbo.Enrollment", "StudentId");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EnrollmentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Person", "HireDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Person", "Name", c => c.String());
            DropColumn("dbo.Person", "Discriminator");
            DropColumn("dbo.Person", "EnrollmentDate");
            RenameTable(name: "dbo.Person", newName: "Instructor");
        }
    }
}
