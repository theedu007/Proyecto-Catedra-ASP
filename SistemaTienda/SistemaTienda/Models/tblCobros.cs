using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaTienda.Models
{
    public class tblCobros
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int? id_cxc { get; set; }

        [Required]
        public DateTime? fecha { get; set; }

        [Required]
        public Decimal? abono { get; set; }

        [ForeignKey("id_cxc")]
        public virtual tblCxC tblCxC { get; set; }
    }
}