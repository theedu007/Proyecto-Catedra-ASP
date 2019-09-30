namespace SistemaTienda.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblCxP")]
    public partial class tblCxP
    {
        public int Id { get; set; }

        public int? id_compra { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fecha_limite { get; set; }

        public decimal? abonado { get; set; }

        public virtual tblCompra tblCompra { get; set; }
    }
}
