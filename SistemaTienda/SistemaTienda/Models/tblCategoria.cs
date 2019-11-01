namespace SistemaTienda.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblCategoria")]
    public partial class tblCategoria
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCategoria()
        {
            tblProducto = new HashSet<tblProducto>();
        }

        [Display(Name = "Categoria")]
        public int Id { get; set; }

        [Display(Name = "Nombre Categoria")]
        [StringLength(64)]
        public string nombre_categoria { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblProducto> tblProducto { get; set; }
    }
}
