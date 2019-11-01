namespace SistemaTienda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEstadoColumnCuentas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblCxP", "estado", c => c.String());
            AddColumn("dbo.tblTransacciones", "abonado", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.tblCxC", "estado", c => c.String());
            DropColumn("dbo.tblTransacciones", "pago");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblTransacciones", "pago", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.tblCxC", "estado");
            DropColumn("dbo.tblTransacciones", "abonado");
            DropColumn("dbo.tblCxP", "estado");
        }
    }
}
