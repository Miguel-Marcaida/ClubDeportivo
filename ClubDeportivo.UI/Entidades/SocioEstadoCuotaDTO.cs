using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubDeportivo.UI.Entidades
{
    // Mapea directamente a las columnas de la vista SQL 'v_socios_estado_cuota'
    public class SocioEstadoCuotaDTO
    {
        // =========================================================
        // 1. Datos Clave y Personales (de la vista)
        // =========================================================
        public int IdPersona { get; set; }
        public string Dni { get; set; }

        // Campo CONCATENADO de la vista: p.nombre + ' ' + p.apellido
        public string NombreCompleto { get; set; }

        public string Telefono { get; set; }
        public string Email { get; set; }

        // =========================================================
        // 2. Datos de Socio (de la vista)
        // =========================================================
        public int NumeroCarnet { get; set; }
        public bool EstadoActivo { get; set; }
        public bool EstaVigente { get; set; }

        // =========================================================
        // 3. Datos de Cuotas y Morosidad (CRÍTICO para el reporte)
        // =========================================================

        // El ID de la cuota pendiente más antigua. Es nullable porque puede estar al día (NULL en la vista).
        public int? IdCuotaPendiente { get; set; }

        // Fecha de Vencimiento de la cuota pendiente más antigua. CRÍTICA.
        public DateTime? FechaVencimientoPendiente { get; set; }

        // Monto de la cuota pendiente.
        public decimal? MontoCuotaPendiente { get; set; }

        // Fecha del último pago realizado (para "VER ESTADO DE CUENTA"). Es nullable.
        public DateTime? FechaPagoUltima { get; set; }

        // Días de mora calculados en la vista (DATEDIFF). Si es positivo, es moroso. Si es 0, vence hoy.
        // Es nullable si no tiene cuota pendiente.
        //public int? DiasMora { get; set; }
        public int MesesMora { get; set; }
        // =========================================================
        // 4. Propiedades Derivadas (Opcionales para la UI)
        // =========================================================

        // Propiedad de solo lectura para saber si es un moroso 'real' (más de 0 días vencido)
        public bool EsMoroso => MesesMora > 0;

        // Propiedad de solo lectura para saber si vence hoy
        //public bool VenceHoy => DiasMora.HasValue && DiasMora == 0;
    }
}