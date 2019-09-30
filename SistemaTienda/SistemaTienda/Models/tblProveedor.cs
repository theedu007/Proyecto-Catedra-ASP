namespace SistemaTienda.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblProveedor")]
    public partial class tblProveedor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblProveedor()
        {
            tblCompra = new HashSet<tblCompra>();
        }

        public int Id { get; set; }

        [StringLength(64)]
        public string nombre { get; set; }

        [StringLength(128)]
        public string nombre_contacto { get; set; }

        [StringLength(64)]
        public string cargo_contacto { get; set; }

        [StringLength(64)]
        public string direccion { get; set; }

        [StringLength(64)]
        public string telefono { get; set; }

        [StringLength(64)]
        public string email { get; set; }

        [StringLength(255)]
        public string ruta_imagen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCompra> tblCompra { get; set; }
    }
}
