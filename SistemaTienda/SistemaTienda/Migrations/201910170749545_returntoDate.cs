namespace SistemaTienda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class returntoDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblCxP", "fecha_limite", c => c.DateTime(storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblCxP", "fecha_limite", c => c.DateTime());
        }
    }
}
