using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubDeportivo.UI.Utilitarios
{
    /// <summary>
    /// Contiene todas las constantes de texto utilizadas en la interfaz de usuario (UI)
    /// y en los mensajes de sistema.
    /// </summary>
    public static class MensajesUI
    {
        // --- TEXTOS DE BOTONES ESTÁNDAR (Existentes) ---
        public const string BOTON_ACEPTAR = "Aceptar";
        public const string BOTON_CANCELAR = "Cancelar";
        public const string BOTON_SI = "Sí";
        public const string BOTON_NO = "No";

        // --- TÍTULOS DE DIÁLOGO POR DEFECTO (Existentes) ---
        public const string TITULO_EXITO = "Éxito";
        public const string TITULO_ERROR = "Error";
        public const string TITULO_ADVERTENCIA = "Advertencia";
        public const string TITULO_CONFIRMAR_ACCION = "Confirmar Acción";

        // --- FrmLogin: TÍTULOS Y PLACEHOLDERS (Nuevos) ---
        public const string LOGIN_TITULO_FORM = "ACCESO AL SISTEMA";
        public const string LOGIN_PLACEHOLDER_USUARIO = "Usuario";
        public const string LOGIN_PLACEHOLDER_PASS = "Contraseña";
        public const string LOGIN_BOTON_INGRESAR = "INGRESAR";

        // --- FrmLogin: MENSAJES DE VALIDACIÓN Y SISTEMA (Nuevos) ---
        public const string LOGIN_MSG_CAMPOS_REQUERIDOS = "Debe ingresar usuario y contraseña válidos.";
        public const string LOGIN_TITULO_ACCESO_DENEGADO = "Error de Acceso";
        public const string LOGIN_MSG_ACCESO_DENEGADO = "Usuario y/o contraseña incorrectos.";
        public const string LOGIN_TITULO_ACCESO_CONCEDIDO = "Acceso Concedido";
        public const string LOGIN_MSG_BIENVENIDA_PARTE1 = "Bienvenido/a al Sistema de Gestion de club Deportivo, ";
        public const string LOGIN_TITULO_ERROR_CRITICO = "Error Crítico";
        public const string LOGIN_MSG_ERROR_CRITICO = "Ocurrió un error de sistema: {0}\nVerifique la conexión a MySQL.";

        // Nota: Los mensajes dinámicos usan placeholders como {0} si son necesarios.
    }
}
