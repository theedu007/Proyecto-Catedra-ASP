namespace SistemaTienda.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblKardex")]
    public partial class tblKardex
    {
        public int Id { get; set; }

        public int? id_producto { get; set; }

        public int? id_compra { get; set; }

        public int? id_venta { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fecha { get; set; }

        public virtual tblProducto tblProducto { get; set; }
    }
}
