using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubDeportivo.UI.Entidades
{
    public class RegistroAcceso
    {
        public int IdRegistro { get; set; }
        public int IdPersona { get; set; } // FK a no_socios(id_persona)
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; } // Fecha y hora del acceso
        public string FormaPago { get; set; }

        public int? IdActividad { get; set; } 
        public RegistroAcceso() { }

    }
}
