using PdfSharp.Fonts;

namespace ClubDeportivo.UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
           

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Línea que soluciona el error de fuentes en .NET 8.0:
           // GlobalFontSettings.FontResolver = PdfSharp.Fonts.DefaultFontResolver.Global;

            Application.Run(new FrmLogin());
            //Application.Run(new FrmRegistrarPagos(0));
        }
    }
}