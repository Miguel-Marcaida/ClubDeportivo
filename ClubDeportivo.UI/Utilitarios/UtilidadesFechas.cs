using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubDeportivo.UI.Utilitarios
{
    /// <summary>
    /// Clase estática para encapsular métodos de ayuda relacionados con la manipulación y cálculo de fechas.
    /// Esta clase reside en la capa de Utilitarios, siendo accesible por el resto de capas (como BLL).
    /// </summary>
    public static class UtilidadesFechas
    {
        /// <summary>
        /// Calcula la diferencia total en meses completos entre dos fechas.
        /// Este cálculo ignora el componente de día, centrándose únicamente en el año y el mes.
        /// </summary>
        /// <param name="fechaInicio">La fecha de inicio del período.</param>
        /// <param name="fechaFin">La fecha de fin del período.</param>
        /// <returns>El número entero de meses completos entre las dos fechas. Retorna cero si la fecha de inicio es posterior a la de fin.</returns>
        public static int CalcularMesesEntreFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            // Si la fecha de inicio es posterior a la de fin, no hay meses de diferencia.
            if (fechaInicio > fechaFin)
            {
                return 0;
            }

            // La fórmula para calcular la diferencia total de meses es:
            // (Diferencia de años * 12) + (Diferencia de meses)
            int diferenciaMeses = (fechaFin.Year - fechaInicio.Year) * 12 + (fechaFin.Month - fechaInicio.Month);

            // Ej: 2024-01-15 a 2024-02-01 = 1 mes.
            // Ej: 2023-12-01 a 2024-01-01 = 1 mes.

            return diferenciaMeses;
        }
    }
}
