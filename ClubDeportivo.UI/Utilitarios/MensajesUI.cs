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

        // --- FrmPrincipal: TÍTULOS Y ESTADO (Nuevos) ---
        public const string PRINCIPAL_TITULO_SISTEMA = "CLUB DEPORTIVO - SISTEMA DE GESTIÓN";
        public const string PRINCIPAL_STATUS_CARGANDO = "Cargando...";
        public const string PRINCIPAL_STATUS_USUARIO = "Usuario Conectado: **{0}** ({1})"; // {0}=Nombre, {1}=Rol

        // --- FrmPrincipal: TEXTOS DE MENÚ (Nuevos) ---
        public const string MENU_GESTION_PERSONAS = "  Gestión de Personas";
        public const string MENU_INSCRIPCION = "  Inscripción ";
        public const string MENU_REGISTRO_PAGOS = "  Registro de Pagos";
        public const string MENU_GESTION_ACTIVIDADES = "  Gestión de Actividades";
        public const string MENU_LISTADO_MOROSOS = "  Listado de Morosos";
        public const string MENU_CONFIGURACION = "  Configuracion";
        public const string MENU_GESTION_USUARIOS = "  Gestión de Usuarios";
        public const string MENU_CERRAR_SESION = "  Cerrar Sesión";

        // --- FrmPrincipal: MENSAJES DE CONFIRMACIÓN (Nuevos) ---
        public const string PRINCIPAL_TITULO_CONFIRMAR_SALIDA = "Confirmación de Salida";
        public const string PRINCIPAL_MSG_CONFIRMAR_SALIDA = "¿Está seguro que desea salir del sistema y cerrar la aplicación?";
        public const string PRINCIPAL_TITULO_CONFIRMAR_CIERRE_SESION = "Confirmación de Cierre";
        public const string PRINCIPAL_MSG_CONFIRMAR_CIERRE_SESION = "¿Está seguro que desea cerrar la sesión actual?";

    }
}
