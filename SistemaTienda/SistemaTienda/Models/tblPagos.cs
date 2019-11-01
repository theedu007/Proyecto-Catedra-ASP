using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaTienda.Models
{
    public class tblPagos
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int? id_cxp { get; set; }

        [Required]
        public DateTime? fecha { get; set; }

        [Required]
        public Decimal? abono { get; set; }

        [ForeignKey("id_cxp")]
        public virtual tblCxP tblCxP { get; set; }
    }
}