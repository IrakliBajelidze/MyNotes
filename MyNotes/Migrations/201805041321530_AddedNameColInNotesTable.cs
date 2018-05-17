namespace MyNotes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNameColInNotesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notes", "Name", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notes", "Name");
        }
    }
}
