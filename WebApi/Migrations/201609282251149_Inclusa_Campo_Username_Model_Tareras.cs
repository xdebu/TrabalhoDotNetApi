namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inclusa_Campo_Username_Model_Tareras : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tarefas", "Username", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tarefas", "Username");
        }
    }
}
