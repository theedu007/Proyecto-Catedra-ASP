namespace SistemaTienda.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tblDevoluciones
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblDevoluciones()
        {
            tblKardex = new HashSet<tblKardex>();
        }

        public int Id { get; set; }

        public int? id_compra { get; set; }

        public int? id_venta { get; set; }

        public int? cantidad { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fecha { get; set; }

        public virtual tblCompra tblCompra { get; set; }

        public virtual tblVenta tblVenta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblKardex> tblKardex { get; set; }
    }
}
