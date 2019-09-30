namespace SistemaTienda.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblCxC")]
    public partial class tblCxC
    {
        public int Id { get; set; }

        public int? id_venta { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fecha_limite { get; set; }

        public decimal? abonado { get; set; }

        public virtual tblVenta tblVenta { get; set; }
    }
}
