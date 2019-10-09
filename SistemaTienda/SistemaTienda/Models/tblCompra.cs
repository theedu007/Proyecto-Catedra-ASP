namespace SistemaTienda.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblCompra")]
    public partial class tblCompra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCompra()
        {
            tblDevoluciones = new HashSet<tblDevoluciones>();
            tblCxP = new HashSet<tblCxP>();
        }

        public int Id { get; set; }

        public int id_proveedor { get; set; }

        public int id_empleado { get; set; }

        public int id_producto { get; set; }

        public int id_metodopago { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha { get; set; }

        public int cantidad_compra { get; set; }

        public decimal precio_compra { get; set; }

        public virtual tblEmpleado tblEmpleado { get; set; }

        public virtual tblProveedor tblProveedor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblDevoluciones> tblDevoluciones { get; set; }

        public virtual tblMetodoPago tblMetodoPago { get; set; }

        public virtual tblProducto tblProducto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCxP> tblCxP { get; set; }
    }
}
