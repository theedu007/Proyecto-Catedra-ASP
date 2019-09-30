namespace SistemaTienda.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblVenta")]
    public partial class tblVenta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblVenta()
        {
            tblCxC = new HashSet<tblCxC>();
        }

        public int Id { get; set; }

        public int id_cliente { get; set; }

        public int id_empleado { get; set; }

        public int id_producto { get; set; }

        public int id_metodopago { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha { get; set; }

        public int cantidad_venta { get; set; }

        public decimal precio_final { get; set; }

        public virtual tblCliente tblCliente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCxC> tblCxC { get; set; }

        public virtual tblEmpleado tblEmpleado { get; set; }

        public virtual tblMetodoPago tblMetodoPago { get; set; }

        public virtual tblProducto tblProducto { get; set; }
    }
}
