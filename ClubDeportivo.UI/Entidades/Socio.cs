using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubDeportivo.UI.Entidades
{
    public class Socio:Persona
    {
        public int NumeroCarnet { get; set; }
        public bool EstadoActivo { get; set; }
        public bool FichaMedicaEntregada { get; set; }

        public Socio() { }
    }
}
