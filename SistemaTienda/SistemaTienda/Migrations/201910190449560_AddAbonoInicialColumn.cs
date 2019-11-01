namespace SistemaTienda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAbonoInicialColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblCxP", "abono_inicial", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.tblCxC", "abono_inicial", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblCxC", "abono_inicial");
            DropColumn("dbo.tblCxP", "abono_inicial");
        }
    }
}
