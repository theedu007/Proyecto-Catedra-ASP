namespace SistemaTienda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblPagoChangedName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.tblPagos", newName: "tblTransacciones");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.tblTransacciones", newName: "tblPagos");
        }
    }
}
