using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaTienda.Models
{
    public class KardexRow
    {
        public DateTime? fecha { get; set; }

        public int? id_compra { get; set; }
        public int? id_venta { get; set; }

        public int? entrada_Q { get; set; }
        public Decimal? entrada_cu { get; set; }
        public Decimal? entrada_ct { get; set; }

        public int? salida_Q { get; set; }
        public Decimal? salida_cu { get; set; }
        public Decimal? salida_ct { get; set; }

        public int? saldo_Q { get; set; }
        public Decimal? saldo_cu { get; set; }
        public Decimal? saldo_ct { get; set; }
    }


}