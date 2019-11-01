namespace SistemaTienda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class disableDateCxP : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblCxP", "fecha_limite", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblCxP", "fecha_limite", c => c.DateTime(storeType: "date"));
        }
    }
}
