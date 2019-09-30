namespace SistemaTienda.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblCliente")]
    public partial class tblCliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCliente()
        {
            tblVenta = new HashSet<tblVenta>();
        }

        public int Id { get; set; }

        [StringLength(64)]
        public string nombre_compañia { get; set; }

        [StringLength(64)]
        public string direccion { get; set; }

        [StringLength(64)]
        public string telefono { get; set; }

        [StringLength(64)]
        public string email { get; set; }

        [StringLength(255)]
        public string ruta_imagen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblVenta> tblVenta { get; set; }
    }
}
