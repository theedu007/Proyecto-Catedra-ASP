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
        public tblCxC()
        {
            tblCobros = new HashSet<tblCobros>();
        }

        [Display(Name = "CxC")]
        public int Id { get; set; }

        public int? id_venta { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fecha_limite { get; set; }

        public decimal? abono_inicial { get; set; }

        public decimal? abonado { get; set; }

        public String estado { get; set; }

        public virtual tblVenta tblVenta { get; set; }

        public ICollection<tblCobros> tblCobros { get; set; }
    }
}