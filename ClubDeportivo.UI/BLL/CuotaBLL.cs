using ClubDeportivo.UI.DAL;
using ClubDeportivo.UI.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubDeportivo.UI.BLL
{
    public class CuotaBLL
    {
        private readonly CuotaDAL oCuotaDAL = new CuotaDAL();

        // ** 1. Método CRÍTICO: Registrar Pago (incluye lógica de vencimiento) **
        public int RegistrarPagoDeCuota(Cuota oCuota)
        {
            // Lógica de Negocio 1: Validación básica
            if (oCuota.Monto <= 0)
            {
                throw new Exception("El monto de la cuota debe ser mayor a cero.");
            }

            // AÑADIDO: Validación de Concepto (Lógica de Negocio Crítica)
            if (string.IsNullOrEmpty(oCuota.Concepto))
            {
                throw new Exception("El Concepto de la cuota es obligatorio para el registro contable.");
            }
            // Lógica de Negocio 2: Establecer la fecha de pago si no está definida
            // En la inscripción, asumimos que siempre es HOY.
            if (oCuota.FechaPago == null)
            {
                oCuota.FechaPago = DateTime.Today;
            }

            // Lógica de Negocio 3: Determinar Fecha de Vencimiento
            // Si la fecha de vencimiento viene vacía, la BLL la calcula (ej. 30 días después del pago)
            if (oCuota.FechaVencimiento == DateTime.MinValue)
            {
                // A. Nos enfocamos en el inicio del mes para asegurar que el cálculo sea siempre un mes completo.
                DateTime primerDiaDelMesDePago = new DateTime(oCuota.FechaPago.Value.Year, oCuota.FechaPago.Value.Month, 1);

                // B. La cuota pagada cubre el mes en curso y vence al final del mes siguiente.
                // Ejemplo: Si paga el 29/10, cubre Octubre y vence el 30/11.

                // Calculamos: Primer día del mes de pago + 2 meses - 1 día = Final del mes siguiente.
                oCuota.FechaVencimiento = primerDiaDelMesDePago.AddMonths(2).AddDays(-1);
            }

            // Llamada a la DAL
            return oCuotaDAL.RegistrarPagoCuota(oCuota);
        }

        
    }
}
