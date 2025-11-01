using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubDeportivo.UI.Entidades
{
    /// <summary>
    /// Objeto de Transferencia de Datos específico para la pantalla de Registro de Pago.
    /// Contiene solo los datos cruciales para la toma de decisiones y visualización.
    /// </summary>
    public class PersonaPagoDetalleDTO
    {
        public int IdPersona { get; set; }
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        // 🚨 PROPIEDAD REQUERIDA PARA LA REIMPRESIÓN DEL CARNET
        public int? NumeroCarnet { get; set; } // <--- ¡NUEVO CAMPO!

        // Determina la lógica a aplicar (Socio o No Socio)
        public bool EsSocio { get; set; }

        // CRÍTICO: La fecha del último período cubierto (solo aplica a Socios).
        // Si no tiene pagos o no es socio, será DateTime.MinValue (o el valor del DBNull en DAL).
        public DateTime UltimaCuotaCubierta { get; set; }

        // CRÍTICO: Estado calculado por la BLL (AL DÍA, PENDIENTE, N/A).
        public string EstadoMembresia { get; set; }

        public string NombreCompleto {
            get {
                return $"{Nombre} {Apellido}";
            }
        }
    }
}
