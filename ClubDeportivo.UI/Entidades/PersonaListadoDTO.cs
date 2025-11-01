using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubDeportivo.UI.Entidades
{
    // DTO: Objeto de Transferencia de Datos.
    // Usado exclusivamente para transportar datos desde la BLL al FrmGestionPersonas.
    public class PersonaListadoDTO
    {
        // 1. Identificador CRÍTICO (Visible=false en el DataGridView)
        public int IdPersona { get; set; }

        // 2. Datos de Persona
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        // Propiedad calculada para mostrar en una sola columna
        public string NombreCompleto => $"{Nombre} {Apellido}";

        // 3. Tipo y Rol
        public string TipoPersona { get; set; } // Valor: "Socio" o "No Socio"

        // 4. Datos Específicos de Socio
        public int? NumeroCarnet { get; set; } // Usamos int? (nullable int) porque será NULL para No Socios

        // 5. Estado de Pago/Membresía (Calculado en la BLL)
        public string EstadoMembresia { get; set; } // Valor: "AL DÍA", "PENDIENTE", "N/A"
    }
}
