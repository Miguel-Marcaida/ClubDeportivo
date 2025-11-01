using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubDeportivo.UI.Entidades
{
    public class NoSocio:Persona
    {
        // NOTA: Hereda IdPersona, Dni, Nombre, etc.
        public DateTime? FechaPagoDia { get; set; } // Nullable, atributo propio

        public NoSocio() { }

    }
}
