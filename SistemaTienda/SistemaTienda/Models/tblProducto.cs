namespace SistemaTienda.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblProducto")]
    public partial class tblProducto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblProducto()
        {
            tblCompra = new HashSet<tblCompra>();
            tblKardex = new HashSet<tblKardex>();
            tblVenta = new HashSet<tblVenta>();
        }

        public int Id { get; set; }

        public int? id_categoria { get; set; }

        [StringLength(64)]
        public string nombre_producto { get; set; }

        public decimal? precio { get; set; }

        public int? cantidad { get; set; }

        [StringLength(255)]
        public string imagen_producto { get; set; }

        [StringLength(255)]
        public string descripcion { get; set; }

        public virtual tblCategoria tblCategoria { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCompra> tblCompra { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblKardex> tblKardex { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblVenta> tblVenta { get; set; }
    }
}
