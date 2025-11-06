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

        // --- FrmInscripcionClub: TÍTULOS Y ETIQUETAS ---
        public const string INSCRIPCION_TITULO_FORM = "Inscripción Club Deportivo";
        public const string INSCRIPCION_TITULO_VISTA = "REGISTRO DE PERSONA (SOCIO / NO SOCIO)";
        public const string INSCRIPCION_GB_DATOS_PERSONA = "Datos de la Persona";
        public const string INSCRIPCION_GB_TIPO_PERSONA = "Selección de Rol";
        public const string INSCRIPCION_LBL_DNI = "DNI:";
        public const string INSCRIPCION_LBL_NOMBRE = "Nombre:";
        public const string INSCRIPCION_LBL_APELLIDO = "Apellido:";
        public const string INSCRIPCION_LBL_FECHA_NAC = "Fecha de Nacimiento:";
        public const string INSCRIPCION_LBL_TELEFONO = "Teléfono:";
        public const string INSCRIPCION_LBL_EMAIL = "Email:";
        public const string INSCRIPCION_RB_SOCIO = "Socio (Membresía Mensual)";
        public const string INSCRIPCION_RB_NO_SOCIO = "No Socio (Acceso Diario)";
        public const string INSCRIPCION_LBL_CARNET = "Número de Carnet:";
        public const string INSCRIPCION_CHK_FICHA_MEDICA = "Ficha Médica y Apto Físico";
        public const string INSCRIPCION_LBL_FECHA_ACCESO = "Fecha y Hora de Acceso:";
        public const string INSCRIPCION_LBL_HORA_ACTUAL_PLACEHOLDER = "Hora Actual"; // Se usará solo como placeholder inicial
        public const string INSCRIPCION_CHK_APTO_FISICO_NO_SOCIO = "Apto Fisico Para No Socio";
        public const string INSCRIPCION_BTN_REGISTRAR_SOCIO = "REGISTRAR SOCIO Y PAGAR CUOTA N°1";
        public const string INSCRIPCION_BTN_REGISTRAR_NO_SOCIO = "REGISTRAR ACCESO Y COBRAR";
        public const string INSCRIPCION_BTN_CANCELAR = "CANCELAR / CERRAR";


        // --- FrmInscripcionClub: MENSAJES ---
        public const string INSCRIPCION_ERROR_CARNET_CALCULO = "ERROR";
        public const string INSCRIPCION_ERROR_OBTENER_CARNET = "Error al obtener el número de carnet: {0}";
        public const string INSCRIPCION_TITULO_ERROR = "Error";

        // Validaciones
        public const string INSCRIPCION_VALIDACION_CAMPOS_REQUERIDOS = "Los campos DNI, Nombre y Apellido son obligatorios.";
        public const string INSCRIPCION_TITULO_ADVERTENCIA = "Advertencia de Campos Faltantes";
        public const string INSCRIPCION_VALIDACION_DNI_NUMERICO = "El campo DNI debe contener solo números.";
        public const string INSCRIPCION_VALIDACION_TELEFONO_NUMERICO = "El campo Teléfono debe contener solo números (sin guiones/espacios).";
        public const string INSCRIPCION_TITULO_ERROR_FORMATO = "Error de Formato";
        public const string INSCRIPCION_VALIDACION_EMAIL = "El formato del Email es incorrecto. Si no es obligatorio, déjelo vacío.";
        public const string INSCRIPCION_VALIDACION_CARNET_OBLIGATORIO = "El Número de Carnet es obligatorio para el Socio y debe ser numérico.";
        public const string INSCRIPCION_VALIDACION_FICHA_SOCIO = "El Socio debe entregar la ficha medica y apto fisico.";
        public const string INSCRIPCION_VALIDACION_APTO_NO_SOCIO = "El No Socio debe entregar apto fisico.";

        // Socio - Flujo de Pago
        public const string INSCRIPCION_SOCIO_PAGO_TITULO = "Forma de Pago";
        public const string INSCRIPCION_SOCIO_PAGO_MSG = "Seleccione la forma de pago de la PRIMERA CUOTA MENSUAL (Monto: ${0:N2}):";
        public const string INSCRIPCION_SOCIO_PAGO_ALERTA_FALTA = "Debe seleccionar la forma de pago para completar la inscripción de Socio.";
        public const string INSCRIPCION_SOCIO_PAGO_CONFIRMACION_TITULO = "Confirmación de Pago y Registro";
        public const string INSCRIPCION_SOCIO_PAGO_CONFIRMACION_MSG = "Confirma el registro e inscripción del Socio, y el cobro de la PRIMERA CUOTA por un monto de ${0:N2} (pago en {1})?";
        public const string INSCRIPCION_SOCIO_REGISTRO_EXITO_MSG = "¡Socio {0} registrado y PRIMERA CUOTA COBRADA con éxito!\n\nID Persona: {1}\nNúmero de Carnet Asignado: {2}\nMonto Cobrado: ${3:N2} ({4})"; // 0=Nombre, 1=ID, 2=Carnet, 3=Monto, 4=FPago
        public const string INSCRIPCION_SOCIO_REGISTRO_EXITO_TITULO = "Registro de Socio OK";
        public const string INSCRIPCION_SOCIO_ERROR_FORMATO_CARNET = "El Número de Carnet debe ser un valor numérico entero o el valor precalculado es incorrecto.";
        public const string INSCRIPCION_SOCIO_ERROR_INSCRIPCION_TITULO = "Error de Inscripción";

        // No Socio - Flujo de Pago
        public const string INSCRIPCION_NO_SOCIO_PAGO_MSG = "Seleccione la forma de pago del ACCESO DIARIO (Monto: ${0:N2}):";
        public const string INSCRIPCION_NO_SOCIO_PAGO_ALERTA_FALTA = "Debe seleccionar la forma de pago para registrar el acceso diario.";
        public const string INSCRIPCION_NO_SOCIO_PAGO_CONFIRMACION_TITULO = "Confirmación de Pago y Registro de Acceso";
        public const string INSCRIPCION_NO_SOCIO_PAGO_CONFIRMACION_MSG = "Confirma el registro y acceso diario del No Socio, y el cobro de ${0:N2} (pago en {1})?";
        public const string INSCRIPCION_NO_SOCIO_REGISTRO_EXITO_MSG = "¡No Socio {0} registrado y acceso diario cobrado con éxito!\nID: {1}\nMonto cobrado: ${2:N2} en {3}."; // 0=Nombre, 1=ID, 2=Monto, 3=FPago
        public const string INSCRIPCION_NO_SOCIO_REGISTRO_EXITO_TITULO = "Registro de Acceso OK";
        public const string INSCRIPCION_NO_SOCIO_ERROR_ACCESO_TITULO = "Error de Registro de Acceso";
        public const string INSCRIPCION_NO_SOCIO_CONCEPTO_ACCESO = "Acceso Diario General";

        // PDF
        public const string PDF_CARNET_TITULO_EXITO = "Carnet Generado y Abierto";
        public const string PDF_CARNET_EXITO_MSG = "El Carnet de Socio ha sido generado y ABIERTO correctamente.\n\nRuta de guardado: {0}";
        public const string PDF_CARNET_ERROR_MSG = "Advertencia: El Carnet se generó, pero no se pudo abrir automáticamente.\nError: {0}";
        public const string PDF_COMPROBANTE_TITULO_EXITO = "Comprobante Generado";
        public const string PDF_COMPROBANTE_EXITO_MSG = "El Comprobante de Acceso ha sido generado y ABIERTO (o intentado abrir) correctamente.\n\nRuta de guardado: {0}";
        public const string PDF_COMPROBANTE_ERROR_MSG = "Advertencia: El Comprobante se generó, pero no se pudo abrir automáticamente.\nError: {0}";

    }
}
