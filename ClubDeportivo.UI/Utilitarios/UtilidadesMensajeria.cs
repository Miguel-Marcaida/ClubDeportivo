using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubDeportivo.UI.Utilitarios
{
    // NOTA IMPORTANTE: Esta clase actúa como un intermediario (wrapper).
    // Inicialmente, en los formularios antiguos, llamará a MessageBox.
    // En los nuevos formularios, implementará la lógica de usar los paneles.
    // El poder de esta clase es que si un día se aprueba el cambio,
    // solo modificas *esta* clase para actualizar TODO el sistema.

    public static class UtilidadesMensajeria
    {
       

        /// <summary>
        /// Muestra un mensaje de éxito. Usado después de una operación CRUD exitosa.
        /// </summary>
        /// <param name="textoMensaje">El contenido del mensaje a mostrar.</param>
        public static void MostrarExito(string textoMensaje)
        {
            // === IMPLEMENTACIÓN TEMPORAL (Formularios Antiguos) ===
            MessageBox.Show(textoMensaje, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // =====================================================

            // FUTURA IMPLEMENTACIÓN (Al obtener la aprobación de UX):
            // pnlMensajes.Mostrar(textoMensaje, TipoMensaje.EXITO); 
        }

        /// <summary>
        /// Muestra un mensaje de error crítico o de validación fallida.
        /// </summary>
        /// <param name="textoMensaje">El contenido del error.</param>
        public static void MostrarError(string textoMensaje)
        {
            // === IMPLEMENTACIÓN TEMPORAL (Formularios Antiguos) ===
            MessageBox.Show(textoMensaje, "Error en el Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            // =====================================================

            // FUTURA IMPLEMENTACIÓN:
            // pnlMensajes.Mostrar(textoMensaje, TipoMensaje.ERROR);
        }

        /// <summary>
        /// Muestra un mensaje de advertencia.
        /// </summary>
        /// <param name="textoMensaje">El contenido de la advertencia.</param>
        public static void MostrarAdvertencia(string textoMensaje)
        {
            // === IMPLEMENTACIÓN TEMPORAL (Formularios Antiguos) ===
            MessageBox.Show(textoMensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            // =====================================================

            // FUTURA IMPLEMENTACIÓN:
            // pnlMensajes.Mostrar(textoMensaje, TipoMensaje.ADVERTENCIA);
        }

        /// <summary>
        /// Muestra el panel de comprobante (reservado solo para formularios nuevos, como el de pagos).
        /// </summary>
        /// <param name="idTransaccion">El ID de la transacción o comprobante.</param>
        /// <param name="monto">El monto de la operación.</param>
        public static void MostrarComprobante(string idTransaccion, decimal monto)
        {
            // Este método se usa *exclusivamente* en los formularios modernos.
            // En los formularios antiguos, no tendrá una implementación equivalente.
            string comprobante = $"Comprobante Generado:\nID: {idTransaccion}\nMonto: {monto:C}";
            MessageBox.Show(comprobante, "Comprobante", MessageBoxButtons.OK, MessageBoxIcon.None); // Opcional, solo para demostración.
        }
    }

}

