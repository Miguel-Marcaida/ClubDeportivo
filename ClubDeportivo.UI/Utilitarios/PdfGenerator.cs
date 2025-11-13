using ClubDeportivo.UI.Entidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.IO; // Necesario para guardar el archivo
using PdfSharp.Fonts;


namespace ClubDeportivo.UI.Utilitarios
{

    /// <summary>
        /// Clase estática para generar documentos PDF (Carnet de Socio y Listados)
        /// utilizando la librería PdfSharp.
        /// </summary>
    public static class PdfGenerator
    {
        // Define la ruta donde se guardarán los reportes (dentro de la carpeta "Reportes" del ejecutable)
        private static readonly string RUTA_REPORTES = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reportes");
        private const string FUENTE_BASE = "Helvetica";

        /// <summary>
        /// Asegura que el directorio de reportes exista.
        /// </summary>
        private static void AsegurarDirectorio()
        {
            if (!Directory.Exists(RUTA_REPORTES))
                Directory.CreateDirectory(RUTA_REPORTES);
        }

        // ====================================================================
        // MÉTODO 1: GENERAR CARNET SOCIO
        // Recibe los datos detallados de una persona y genera un carnet en formato pequeño.
        // ====================================================================
        public static string GenerarCarnetSocio(PersonaDetalleDTO detalle)
        {
            // 1. Crear el directorio si no existe
            AsegurarDirectorio(); // Usando el método auxiliar

            // 2. Configuración básica del documento y página (simulando tamaño de carnet)
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Carnet de Socio";

            PdfPage page = document.AddPage();
            page.Width = 243; // Ancho Aprox. 86mm (tamaño CR80)
            page.Height = 153; // Alto Aprox. 54mm

            XGraphics gfx = XGraphics.FromPdfPage(page);

            // 3. Definición de fuentes
            XFont fontTitulo = new XFont(FUENTE_BASE, 12, XFontStyleEx.Bold);
            XFont fontDatos = new XFont(FUENTE_BASE, 9, XFontStyleEx.Regular);
            XFont fontEtiqueta = new XFont(FUENTE_BASE, 8, XFontStyleEx.Bold);

            // 4. Fondo y Título del Club
            XSolidBrush fondo = new XSolidBrush(XColor.FromArgb(70, 80, 150)); // Un azul oscuro/grisáceo
            gfx.DrawRectangle(fondo, 0, 0, page.Width, page.Height);

            gfx.DrawString("CLUB DEPORTIVO", fontTitulo, XBrushes.White,
                    new XRect(10, 10, page.Width - 20, 20), XStringFormats.TopLeft);

            // 5. N° Carnet y DNI
            gfx.DrawString("CARNET N°", fontEtiqueta, XBrushes.LightGray, 10, 35);
            // El número de carnet va en negrita (fontTitulo)
            gfx.DrawString(detalle.NumeroCarnet?.ToString() ?? "N/A", fontTitulo, XBrushes.White,
              new XRect(10, 45, page.Width - 20, 20), XStringFormats.TopLeft);

            // 6. Datos del Socio
            int top = 80;
            gfx.DrawString("NOMBRE:", fontEtiqueta, XBrushes.LightGray, 10, top);
            gfx.DrawString($"{detalle.Nombre} {detalle.Apellido}", fontDatos, XBrushes.White, 10, top + 12);
            top += 25;

            gfx.DrawString("DNI:", fontEtiqueta, XBrushes.LightGray, 10, top);
            gfx.DrawString(detalle.Dni, fontDatos, XBrushes.White, 10, top + 12);

            // 7. Estado de Vigencia
            //string estadoTxt = detalle.EstadoActivo.GetValueOrDefault() ? " " : "INACTIVO / PENDIENTE";
            //XFont fontVigencia = new XFont(FUENTE_BASE, 8, XFontStyleEx.Bold); // Negrita para destacar
            //XBrush estadoBrush = detalle.EstadoActivo.GetValueOrDefault() ? XBrushes.LightGreen : XBrushes.Red;

            //gfx.DrawString(estadoTxt, fontVigencia, estadoBrush,
            //        new XRect(10, page.Height - 20, page.Width - 20, 15),
            //        XStringFormats.BottomLeft);


            // 8. Guardar el documento
            string nombreArchivo = $"Carnet_{detalle.Dni}_{detalle.NumeroCarnet}.pdf";
            string rutaCompleta = Path.Combine(RUTA_REPORTES, nombreArchivo);
            document.Save(rutaCompleta);

            return rutaCompleta;
        }

        // ====================================================================
        // MÉTODO 2: GENERAR LISTADO GENERAL DE PERSONAS
        // Recibe una lista de personas (DTO simplificado) y genera un PDF tabular.
        // ====================================================================
        public static string GenerarListadoGeneral(List<PersonaListadoDTO> listaPersonas)
        {
            // 1. Crear el directorio si no existe
            AsegurarDirectorio(); // Usando el método auxiliar

            // 2. Configuración básica del documento
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Listado General de Personas";
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // 3. Márgenes y Dimensiones
            double margin = 40;
            double yPos = margin;
            double pageHeight = page.Height - margin;
            double pageWidth = page.Width - margin * 2;

            // 4. Definición de fuentes
            XFont fontTitulo = new XFont(FUENTE_BASE, 16, XFontStyleEx.Bold);
            XFont fontHeader = new XFont(FUENTE_BASE, 9, XFontStyleEx.Bold);
            XFont fontBody = new XFont(FUENTE_BASE, 8, XFontStyleEx.Regular);
            double rowHeight = 15;

            // 5. Título del Reporte
            gfx.DrawString("LISTADO GENERAL DE PERSONAS DEL CLUB", fontTitulo, XBrushes.Black,
              new XRect(margin, yPos, pageWidth, 20), XStringFormats.TopCenter);
            yPos += 30;

            // 6. Definición de Columnas y Anchos
            // Definimos anchos fijos para las columnas cortas
            double wDNI = 80;
            double wTipo = 60;
            double wVigencia = 70;

            // El ancho restante se asigna al Nombre Completo
            double wNombre = pageWidth - wDNI - wTipo - wVigencia;

            // Estructura de la tabla (4 columnas)
            string[] headers = { "DNI", "NOMBRE COMPLETO", "TIPO", "ESTADO CUOTA" };
            double[] widths = { wDNI, wNombre, wTipo, wVigencia };
            double currentX = margin;

            XColor headerColor = XColor.FromArgb(220, 220, 220); // Gris claro para el encabezado
            XSolidBrush headerBrush = new XSolidBrush(headerColor);

            // 7. Dibujar los encabezados de Columna
            for (int i = 0; i < headers.Length; i++)
            {
                gfx.DrawRectangle(headerBrush, currentX, yPos, widths[i], 20);
                gfx.DrawString(headers[i], fontHeader, XBrushes.Black,
                        new XRect(currentX, yPos, widths[i], 20), XStringFormats.Center);
                currentX += widths[i];
            }
            yPos += 20;

            // 8. Dibujar Filas de Datos
            int filasProcesadas = 0;

            foreach (PersonaListadoDTO persona in listaPersonas)
            {
                // Manejo de Salto de página
                if (yPos + rowHeight > pageHeight)
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    yPos = margin;
                    currentX = margin;
                    // Redibujar encabezados en la nueva página
                    for (int i = 0; i < headers.Length; i++)
                    {
                        gfx.DrawRectangle(headerBrush, currentX, yPos, widths[i], 20);
                        gfx.DrawString(headers[i], fontHeader, XBrushes.Black,
                                new XRect(currentX, yPos, widths[i], 20), XStringFormats.Center);
                        currentX += widths[i];
                    }
                    yPos += 20;
                }

                currentX = margin;

                string[] datos = {
          persona.Dni ?? "N/D",
          persona.NombreCompleto ?? "N/D",
          persona.TipoPersona ?? "N/D",
          persona.EstadoMembresia ?? "N/D"
        };

                // Dibujar el fondo de la fila (alternando colores para mejor legibilidad)
                XBrush rowBrush = (filasProcesadas % 2 == 0) ? XBrushes.White : new XSolidBrush(XColor.FromArgb(245, 245, 245));
                gfx.DrawRectangle(rowBrush, margin, yPos, pageWidth, rowHeight);

                // Dibujar el texto de las celdas
                for (int i = 0; i < datos.Length; i++)
                {
                    // Pequeño padding de 2px a la izquierda para el texto
                    gfx.DrawString(datos[i], fontBody, XBrushes.Black,
                  new XRect(currentX + 2, yPos, widths[i] - 4, rowHeight), XStringFormats.TopLeft);
                    currentX += widths[i];
                }

                yPos += rowHeight;
                filasProcesadas++;
            }

            // 9. Guardar y retornar la ruta
            string nombreArchivo = $"ListadoGeneral_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            string rutaCompleta = Path.Combine(RUTA_REPORTES, nombreArchivo);
            document.Save(rutaCompleta);

            return rutaCompleta;
        }

        // ====================================================================
        // MÉTODO 3: GENERAR COMPROBANTE DE ACCESO (PARA NO SOCIOS)
        // Genera un comprobante o ticket de acceso diario para No Socios.
        // ====================================================================
        public static string GenerarComprobanteAcceso(string dni, string nombreCompleto, decimal monto, string concepto)
        {
            AsegurarDirectorio();

            // 1. Configuración inicial del documento
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Comprobante de Acceso Diario";

            // Formato de Ticket: ancho reducido para simular un comprobante de caja
            PdfPage page = document.AddPage();
            page.Width = 200; // Ancho reducido para ticket
            page.Height = 350; // Alto suficiente para el contenido

            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont fontHeader = new XFont(FUENTE_BASE, 12, XFontStyleEx.Bold);
            XFont fontData = new XFont(FUENTE_BASE, 10, XFontStyleEx.Regular);
            XFont fontTotal = new XFont(FUENTE_BASE, 14, XFontStyleEx.Bold);

            double yPos = 20;
            double margin = 10;
            double contentWidth = page.Width.Point - (2 * margin);

            // Título
            gfx.DrawString("COMPROBANTE DE ACCESO", fontHeader, XBrushes.Black,
                new XRect(margin, yPos, contentWidth, 15), XStringFormats.TopCenter);
            yPos += 20;

            // Línea separadora
            gfx.DrawLine(XPens.Black, margin, yPos, page.Width.Point - margin, yPos);
            yPos += 15;

            // Datos de la Transacción
            gfx.DrawString($"Fecha/Hora: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", fontData, XBrushes.Black, margin, yPos);
            yPos += 20;

            // Datos del Cliente
            gfx.DrawString("CLIENTE:", fontHeader, XBrushes.Black, margin, yPos);
            yPos += 15;
            gfx.DrawString($"{nombreCompleto}", fontData, XBrushes.Black, margin + 5, yPos);
            yPos += 15;
            gfx.DrawString($"DNI: {dni}", fontData, XBrushes.Black, margin + 5, yPos);
            yPos += 25;

            // Concepto
            gfx.DrawString("CONCEPTO:", fontHeader, XBrushes.Black, margin, yPos);
            yPos += 15;
            gfx.DrawString(concepto, fontData, XBrushes.Black, margin + 5, yPos);
            yPos += 20;

            // Línea separadora
            gfx.DrawLine(XPens.Black, margin, yPos, page.Width.Point - margin, yPos);
            yPos += 15;

            // Total (Destacado en una caja)
            double totalBoxHeight = 30;
            double totalBoxY = yPos;

            // Dibujar recuadro para el Total
            XColor totalColor = XColor.FromArgb(240, 240, 240);
            gfx.DrawRectangle(new XSolidBrush(totalColor), margin, totalBoxY, contentWidth, totalBoxHeight);

            gfx.DrawString("TOTAL COBRADO", fontTotal, XBrushes.DarkRed, margin + 5, totalBoxY + 8);

            // Alineación del monto a la derecha
            gfx.DrawString($"${monto:N2}", fontTotal, XBrushes.DarkRed,
                new XRect(margin, totalBoxY, contentWidth - 5, totalBoxHeight), XStringFormats.CenterRight);

            yPos += totalBoxHeight + 15;

            // Mensaje final
            gfx.DrawString("¡GRACIAS POR SU VISITA!", fontData, XBrushes.Black,
                new XRect(margin, yPos, contentWidth, 15), XStringFormats.TopCenter);


            // 4. Guardar
            string fileName = $"ComprobanteAcceso_{dni}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            string filePath = Path.Combine(RUTA_REPORTES, fileName);
            document.Save(filePath);

            return filePath;
        }


        /// <summary>
        /// Genera un comprobante de pago de Cuota Mensual.
        /// </summary>
        public static string GenerarComprobanteCuota(int idRegistroCuota, string nombreCompleto, string dni,
                                                    decimal monto, string formaPago, DateTime fechaVencimientoAnterior, int mesesPagados)
        {
            AsegurarDirectorio();

            PdfDocument document = new PdfDocument();
            document.Info.Title = "Comprobante de Pago de Cuota";
            PdfPage page = document.AddPage();
            page.Width = 200; // Ancho reducido para ticket
            page.Height = 350;

            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont fontHeader = new XFont(FUENTE_BASE, 12, XFontStyleEx.Bold);
            XFont fontData = new XFont(FUENTE_BASE, 10, XFontStyleEx.Regular);
            XFont fontTotal = new XFont(FUENTE_BASE, 14, XFontStyleEx.Bold);
            XFont fontSmall = new XFont(FUENTE_BASE, 8, XFontStyleEx.Regular);

            double yPos = 20;
            double margin = 10;
            double contentWidth = page.Width.Point - (2 * margin);

            // Título
            gfx.DrawString("COMPROBANTE PAGO CUOTA", fontHeader, XBrushes.Black,
                new XRect(margin, yPos, contentWidth, 15), XStringFormats.TopCenter);
            yPos += 20;
            gfx.DrawLine(XPens.Black, margin, yPos, page.Width.Point - margin, yPos);
            yPos += 15;

            // Datos de la Transacción
            gfx.DrawString($"Nro Reg.: {idRegistroCuota} | Fecha: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", fontData, XBrushes.Black, margin, yPos);
            yPos += 20;

            // Datos del Cliente
            gfx.DrawString("SOCIO:", fontHeader, XBrushes.Black, margin, yPos);
            yPos += 15;
            gfx.DrawString($"{nombreCompleto}", fontData, XBrushes.Black, margin + 5, yPos);
            yPos += 15;
            gfx.DrawString($"DNI: {dni}", fontData, XBrushes.Black, margin + 5, yPos);
            yPos += 25;

            // Concepto
            gfx.DrawString("DETALLE DEL PAGO:", fontHeader, XBrushes.Black, margin, yPos);
            yPos += 15;

            // Cuota Base / Mora
            string detallePago = mesesPagados > 1 ? $"Cuota Mensual (MORA: {mesesPagados} meses)" : "Cuota Mensual";
            gfx.DrawString(detallePago, fontData, XBrushes.Black, margin + 5, yPos);
            yPos += 15;

            // Fecha de Vencimiento ANTERIOR
            gfx.DrawString($"Últ. Cuota Venc.: {fechaVencimientoAnterior:dd/MM/yyyy}", fontSmall, XBrushes.Gray, margin + 5, yPos);
            yPos += 15;

            // Nueva Fecha de Vencimiento (calculada por la BLL/DAL, pero la mostramos aquí)
            DateTime nuevaFechaVencimiento = fechaVencimientoAnterior.AddMonths(mesesPagados);
            gfx.DrawString($"PRÓXIMO VENCIMIENTO: {nuevaFechaVencimiento:dd/MM/yyyy}", fontHeader, XBrushes.Black, margin, yPos);
            yPos += 25;

            // Línea separadora
            gfx.DrawLine(XPens.Black, margin, yPos, page.Width.Point - margin, yPos);
            yPos += 15;

            // Total (Destacado)
            // ... (Lógica de la caja de total idéntica a GenerarComprobanteAcceso) ...
            double totalBoxHeight = 30;
            double totalBoxY = yPos;
            XColor totalColor = XColor.FromArgb(240, 240, 240);
            gfx.DrawRectangle(new XSolidBrush(totalColor), margin, totalBoxY, contentWidth, totalBoxHeight);

            gfx.DrawString("TOTAL COBRADO", fontTotal, XBrushes.DarkRed, margin + 5, totalBoxY + 8);
            gfx.DrawString($"${monto:N2}", fontTotal, XBrushes.DarkRed,
                new XRect(margin, totalBoxY, contentWidth - 5, totalBoxHeight), XStringFormats.CenterRight);

            yPos += totalBoxHeight + 15;

            // Mensaje final
            gfx.DrawString($"Forma de Pago: {formaPago}", fontData, XBrushes.Black,
                new XRect(margin, yPos, contentWidth, 15), XStringFormats.TopCenter);
            yPos += 15;
            gfx.DrawString("¡MEMBRESÍA ACTUALIZADA!", fontHeader, XBrushes.Black,
                new XRect(margin, yPos, contentWidth, 15), XStringFormats.TopCenter);


            // 4. Guardar
            string fileName = $"ComprobanteCuota_{dni}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            string filePath = Path.Combine(RUTA_REPORTES, fileName);
            document.Save(filePath);

            return filePath;
        }
        // ====================================================================
        // MÉTODO 3: GENERAR COMPROBANTE DE ACCESO Y ACTIVIDAD
        // Adaptación para generar un ticket de Acceso Diario que incluye una actividad opcional.
        // ====================================================================
        public static string GenerarComprobanteAccesoActividad(
            int idRegistroAcceso,
            string dni,
            string nombreCompleto,
            decimal montoTotal,
            string formaPago,
            string nombreActividad, // Nombre de la actividad o "Ninguna"
            decimal costoActividad,  // Costo de la actividad (0 si es general)
            decimal costoBase)       // Costo base de acceso (para desglosar)
        {
            AsegurarDirectorio();

            PdfDocument document = new PdfDocument();
            document.Info.Title = "Comprobante de Acceso Diario y Actividad";

            // Formato de Ticket: ancho reducido para simular un comprobante de caja
            PdfPage page = document.AddPage();
            page.Width = 200; // Ancho reducido para ticket
            page.Height = 350; // Alto suficiente para el contenido

            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont fontHeader = new XFont(FUENTE_BASE, 12, XFontStyleEx.Bold);
            XFont fontData = new XFont(FUENTE_BASE, 10, XFontStyleEx.Regular);
            XFont fontTotal = new XFont(FUENTE_BASE, 14, XFontStyleEx.Bold);
            XFont fontSmall = new XFont(FUENTE_BASE, 8, XFontStyleEx.Regular);

            double yPos = 20;
            double margin = 10;
            double contentWidth = page.Width.Point - (2 * margin);

            // Título
            gfx.DrawString("COMPROBANTE DE ACCESO", fontHeader, XBrushes.Black,
                new XRect(margin, yPos, contentWidth, 15), XStringFormats.TopCenter);
            yPos += 20;

            // Línea separadora
            gfx.DrawLine(XPens.Black, margin, yPos, page.Width.Point - margin, yPos);
            yPos += 15;

            // Datos de la Transacción
            gfx.DrawString($"Nro Reg.: {idRegistroAcceso} | Fecha/Hora: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", fontData, XBrushes.Black, margin, yPos);
            yPos += 20;

            // Datos del Cliente
            gfx.DrawString("CLIENTE:", fontHeader, XBrushes.Black, margin, yPos);
            yPos += 15;
            gfx.DrawString($"{nombreCompleto}", fontData, XBrushes.Black, margin + 5, yPos);
            yPos += 15;
            gfx.DrawString($"DNI: {dni}", fontData, XBrushes.Black, margin + 5, yPos);
            yPos += 25;

            // Detalle de Montos
            gfx.DrawString("DETALLE DEL COBRO:", fontHeader, XBrushes.Black, margin, yPos);
            yPos += 15;

            // Monto Base
            gfx.DrawString($"Acceso Diario: ${costoBase:N2}", fontData, XBrushes.Black, margin + 5, yPos);
            yPos += 15;

            // Monto Actividad (si aplica)
            gfx.DrawString($"Actividad: {nombreActividad} (${costoActividad:N2})", fontData, XBrushes.Black, margin + 5, yPos);
            yPos += 20;


            // Línea separadora
            gfx.DrawLine(XPens.Black, margin, yPos, page.Width.Point - margin, yPos);
            yPos += 15;

            // Total (Destacado en una caja)
            // ... (Lógica de la caja de total) ...
            double totalBoxHeight = 30;
            double totalBoxY = yPos;
            XColor totalColor = XColor.FromArgb(240, 240, 240);
            gfx.DrawRectangle(new XSolidBrush(totalColor), margin, totalBoxY, contentWidth, totalBoxHeight);

            gfx.DrawString("TOTAL COBRADO", fontTotal, XBrushes.DarkRed, margin + 5, totalBoxY + 8);
            gfx.DrawString($"${montoTotal:N2}", fontTotal, XBrushes.DarkRed,
                new XRect(margin, totalBoxY, contentWidth - 5, totalBoxHeight), XStringFormats.CenterRight);

            yPos += totalBoxHeight + 15;

            // Mensaje final
            gfx.DrawString($"Forma de Pago: {formaPago}", fontData, XBrushes.Black,
                new XRect(margin, yPos, contentWidth, 15), XStringFormats.TopCenter);
            yPos += 15;
            gfx.DrawString("¡GRACIAS POR SU VISITA!", fontHeader, XBrushes.Black,
                new XRect(margin, yPos, contentWidth, 15), XStringFormats.TopCenter);


            // 4. Guardar
            string fileName = $"ComprobanteAcceso_{dni}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            string filePath = Path.Combine(RUTA_REPORTES, fileName);
            document.Save(filePath);

            return filePath;
        }

        



        // ====================================================================
        // MÉTODO 3: GENERAR LISTADO DE CUOTAS Y MOROSIDAD (Versión Final Definitiva)
        // CORRECCIÓN: Altura del área de dibujo del Título aumentada a 60 puntos.
        // ====================================================================
        public static string GenerarListadoCuotasMorosidad(
            List<SocioEstadoCuotaDTO> listaSocios,
            string tituloReporte)
        {
            // 1. Crear el directorio si no existe (Asumiendo constantes)
            AsegurarDirectorio();

            // 2. Configuración básica del documento
            PdfDocument document = new PdfDocument();
            document.Info.Title = tituloReporte;
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // 3. Márgenes y Dimensiones
            double margin = 40;
            double yPos = margin;
            double pageHeight = page.Height - margin;
            double pageWidth = page.Width - margin * 2; // 515 puntos

            // Asumiendo que FUENTE_BASE es una constante definida (e.g., "Verdana")
            const string FUENTE_BASE = "Verdana";

            // 4. Definición de fuentes 
            XFont fontTitulo = new XFont(FUENTE_BASE, 16, XFontStyleEx.Bold);
            XFont fontHeader = new XFont(FUENTE_BASE, 8.5, XFontStyleEx.Bold);
            XFont fontBody = new XFont(FUENTE_BASE, 8, XFontStyleEx.Regular);
            double rowHeight = 15;

            // 5. Título del Reporte (Dinámico)
            // 🚨 CORRECCIÓN CLAVE: Aumentamos la altura del área de dibujo a 60 puntos
            gfx.DrawString(tituloReporte.ToUpper(), fontTitulo, XBrushes.Black,
                new XRect(margin, yPos, pageWidth, 60), XStringFormats.TopCenter); // <--- Altura en 60
            yPos += 70; // <--- Espaciado posterior en 70

            // 6. Definición de Columnas y Anchos 
            double wCarnet = 50;
            double wDNI = 80;
            double wMonto = 70;
            double wMora = 70;
            double wVencimiento = 80;

            double wNombre = 165;

            // Encabezado de Monto reducido
            string montoHeader = tituloReporte.Contains("VENCIMIENTO HOY") ? "TOTAL ($)" : "DEUDA ($)";

            string[] headers = {
        "CARNET", "DNI", "NOMBRE COMPLETO", "F. VENC. ANT.", montoHeader, "MESES MORA"
    };
            double[] widths = { wCarnet, wDNI, wNombre, wVencimiento, wMonto, wMora };
            double currentX = margin;

            XColor headerColor = XColor.FromArgb(220, 220, 220);
            XSolidBrush headerBrush = new XSolidBrush(headerColor);

            // 7. Dibujar los encabezados de Columna
            for (int i = 0; i < headers.Length; i++)
            {
                gfx.DrawRectangle(headerBrush, currentX, yPos, widths[i], 20);

                // Alineación TopLeft + margen (4)
                gfx.DrawString(headers[i], fontHeader, XBrushes.Black,
                    new XRect(currentX + 4, yPos, widths[i] - 8, 20), XStringFormats.TopLeft);

                currentX += widths[i];
            }
            yPos += 20;

            // 8. Dibujar Filas de Datos
            int filasProcesadas = 0;

            foreach (SocioEstadoCuotaDTO socio in listaSocios)
            {
                // Manejo de Salto de página
                if (yPos + rowHeight > pageHeight)
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    yPos = margin + 70; // Redibujar con el nuevo espaciado del título
                    currentX = margin;
                    // Redibujar encabezados en la nueva página
                    for (int i = 0; i < headers.Length; i++)
                    {
                        gfx.DrawRectangle(headerBrush, currentX, yPos, widths[i], 20);
                        // Usar TopLeft + margen de 4 en el encabezado
                        gfx.DrawString(headers[i], fontHeader, XBrushes.Black,
                            new XRect(currentX + 4, yPos, widths[i] - 8, 20), XStringFormats.TopLeft);
                        currentX += widths[i];
                    }
                    yPos += 20;
                }

                currentX = margin;

                // Formateo de datos específicos
                string fechaVencimiento = socio.FechaVencimientoPendiente?.ToString("dd/MM/yyyy") ?? "-";
                string monto = socio.MontoCuotaPendiente?.ToString("N2") ?? "-";
                string mesesMora = socio.MesesMora > 0 ? socio.MesesMora.ToString() : "-";

                if (tituloReporte.Contains("VENCIMIENTO HOY") && socio.MesesMora == 0)
                {
                    mesesMora = "HOY";
                }

                string[] datos = {
            socio.NumeroCarnet.ToString(),
            socio.Dni ?? "-",
            socio.NombreCompleto ?? "-",
            fechaVencimiento,
            monto,
            mesesMora
        };

                // Dibujar el fondo de la fila
                XBrush rowBrush = (filasProcesadas % 2 == 0) ? XBrushes.White : new XSolidBrush(XColor.FromArgb(245, 245, 245));
                gfx.DrawRectangle(rowBrush, margin, yPos, pageWidth, rowHeight);

                // Dibujar el texto de las celdas
                for (int i = 0; i < datos.Length; i++)
                {
                    XStringFormat format = (i >= 4) ? XStringFormats.Center : XStringFormats.TopLeft;

                    // Aplicación del margen de 4 para que no se pegue
                    gfx.DrawString(datos[i], fontBody, XBrushes.Black,
                        new XRect(currentX + 4, yPos, widths[i] - 8, rowHeight), format);

                    currentX += widths[i];
                }

                yPos += rowHeight;
                filasProcesadas++;
            }

            // 9. Guardar y retornar la ruta
            string nombreArchivo = $"{tituloReporte.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            string rutaCompleta = Path.Combine(RUTA_REPORTES, nombreArchivo);
            document.Save(rutaCompleta);

            return rutaCompleta;
        }

    }

}
