using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubDeportivo.UI.Entidades
{
    public class Cuota
    {
        public int IdCuota { get; set; }
        public int IdPersona { get; set; } // FK a socios(id_persona)
        public decimal Monto { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime? FechaPago { get; set; } // Nullable (si no se ha pagado)
        public string FormaPago { get; set; }

        public string Concepto { get; set; } // <<<<< ¡NUEVA PROPIEDAD REQUERIDA!

        public Cuota() { }
    }
}
