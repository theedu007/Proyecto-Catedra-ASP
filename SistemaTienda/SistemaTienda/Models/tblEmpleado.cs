namespace SistemaTienda.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblEmpleado")]
    public partial class tblEmpleado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblEmpleado()
        {
            tblCompra = new HashSet<tblCompra>();
            tblVenta = new HashSet<tblVenta>();
        }

        public int Id { get; set; }

        public int? id_cargo { get; set; }

        [StringLength(64)]
        public string nombre { get; set; }

        [StringLength(64)]
        public string apellido { get; set; }

        [StringLength(10)]
        public string dui { get; set; }

        [StringLength(17)]
        public string nit { get; set; }

        public int? edad { get; set; }

        [StringLength(64)]
        public string direccion { get; set; }

        [StringLength(64)]
        public string telefono { get; set; }

        [StringLength(64)]
        public string email { get; set; }

        [StringLength(255)]
        public string ruta_imagen { get; set; }

        public virtual tblCargo tblCargo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCompra> tblCompra { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblVenta> tblVenta { get; set; }
    }
}
