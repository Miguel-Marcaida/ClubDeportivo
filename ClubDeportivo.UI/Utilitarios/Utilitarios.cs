using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubDeportivo.UI.Utilitarios
{

    // Recordatorio: EstilosGlobales debe importarse de forma implícita o estar definido en otro archivo
    // en el mismo namespace para que funcione correctamente.

    public static class Utilitarios
    {
        // =================================================================
        // 1. UTILITARIO DE PRESENTACIÓN: FORZAR CENTRADO (LÓGICA CORREGIDA)
        // =================================================================

        public static void ForzarCentrado(Form frm, Panel pnlContenido, int anchoMenuLateral)
        {
            // 1. Limpiar Anclajes y Docking
            pnlContenido.Anchor = AnchorStyles.None;
            pnlContenido.Dock = DockStyle.None;

            // 2. Calcular el espacio de trabajo disponible
            int anchoDisponible = frm.ClientSize.Width;
            int altoDisponible = frm.ClientSize.Height;

            // 3. Centrado Horizontal (Base)
            int xCentradoBase = (anchoDisponible - pnlContenido.Width) / 2;

            // 4. Centrado Vertical
            int yCentrado = (altoDisponible - pnlContenido.Height) / 2;

            // 5. CRÍTICO: Compensación del Menú Lateral (Causa de la confusión anterior)
            int correccionX = anchoMenuLateral / 2;

            // 6. Aplicar Corrección del Menú
            int xFinal = xCentradoBase + correccionX; // <--- CORRECCIÓN APLICADA AQUÍ

            if (xFinal < 5)
            {
                xFinal = 5;
            }

            if (yCentrado < 5)
            {
                yCentrado = 5;
            }

            // Aplicar la ubicación corregida
            pnlContenido.Location = new Point(xFinal, yCentrado);
        }
    }


    // =================================================================
    // 2. UTILITARIO DE INTERACCIÓN: CLASE PROMPT (Simula diálogos)
    // =================================================================

    

}
