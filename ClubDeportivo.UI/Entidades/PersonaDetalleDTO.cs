using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubDeportivo.UI.Entidades
{
    public class PersonaDetalleDTO
    {
        // =========================================================
        // 1. Datos Clave
        // =========================================================
        public int IdPersona { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public bool EstaVigente { get; set; }

        // =========================================================
        // 2. Datos de Socio/No Socio (Campos Nullable)
        // =========================================================
        public bool EsSocio { get; set; }
        public int? NumeroCarnet { get; set; }
        public int? IdSocio { get; set; }
        public bool? EstadoActivo { get; set; }
        public bool? FichaMedicaEntregada { get; set; }
        public DateTime? FechaPagoDia { get; set; }

    }
}
