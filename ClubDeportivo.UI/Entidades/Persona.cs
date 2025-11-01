using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubDeportivo.UI.Entidades
{
    
    public abstract class Persona
    {
        public int IdPersona { get; set; } // PK en la tabla 'personas'
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; } // Nullable
        public string Telefono { get; set; }
        public string Email { get; set; }

        
        public Persona() { }

        // Método de tu diagrama
        public string ObtenerNombreCompleto()
        {
            return $"{Nombre} {Apellido}";
        }
    }
}
