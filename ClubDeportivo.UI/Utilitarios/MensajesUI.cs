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
        // =================================================================
        //                 BLOQUE 1: TÍTULOS Y BOTONES BASE (GENERALES)
        // =================================================================

        // --- TEXTOS DE BOTONES ESTÁNDAR ---
        public const string BOTON_ACEPTAR = "Aceptar";
        public const string BOTON_CANCELAR = "Cancelar";
        public const string BOTON_SI = "Sí";
        public const string BOTON_NO = "No";

        // --- TÍTULOS DE DIÁLOGO POR DEFECTO ---
        public const string TITULO_EXITO = "Éxito";
        public const string TITULO_ERROR = "Error";
        public const string TITULO_ADVERTENCIA = "Advertencia";
        public const string TITULO_CONFIRMAR_ACCION = "Confirmar Acción";

        // =================================================================
        //                BLOQUE 2: FrmLogin (ACCESO)
        // =================================================================

        // --- TÍTULOS Y PLACEHOLDERS ---
        public const string LOGIN_TITULO_FORM = "ACCESO AL SISTEMA";
        public const string LOGIN_PLACEHOLDER_USUARIO = "Usuario";
        public const string LOGIN_PLACEHOLDER_PASS = "Contraseña";
        public const string LOGIN_BOTON_INGRESAR = "INGRESAR";

        // --- MENSAJES DE VALIDACIÓN Y SISTEMA ---
        public const string LOGIN_MSG_CAMPOS_REQUERIDOS = "Debe ingresar usuario y contraseña válidos.";
        public const string LOGIN_TITULO_ACCESO_DENEGADO = "Error de Acceso";
        public const string LOGIN_MSG_ACCESO_DENEGADO = "Usuario y/o contraseña incorrectos.";
        public const string LOGIN_TITULO_ACCESO_CONCEDIDO = "Acceso Concedido";
        public const string LOGIN_MSG_BIENVENIDA_PARTE1 = "Bienvenido/a al Sistema de Gestion de club Deportivo, ";
        public const string LOGIN_TITULO_ERROR_CRITICO = "Error Crítico";
        public const string LOGIN_MSG_ERROR_CRITICO = "Ocurrió un error de sistema: {0}\nVerifique la conexión a MySQL.";


        // =================================================================
        //                BLOQUE 3: FrmPrincipal (MENÚ Y SALIDA)
        // =================================================================

        // --- TÍTULOS Y ESTADO ---
        public const string PRINCIPAL_TITULO_SISTEMA = "CLUB DEPORTIVO - SISTEMA DE GESTIÓN";
        public const string PRINCIPAL_STATUS_CARGANDO = "Cargando...";
        public const string PRINCIPAL_STATUS_USUARIO = "Usuario Conectado: **{0}** ({1})"; // {0}=Nombre, {1}=Rol

        // --- TEXTOS DE MENÚ ---
        public const string MENU_GESTION_PERSONAS = "  Gestión de Personas";
        public const string MENU_INSCRIPCION = "  Inscripción ";
        public const string MENU_REGISTRO_PAGOS = "  Registro de Pagos";
        public const string MENU_GESTION_ACTIVIDADES = "  Gestión de Actividades";
        public const string MENU_LISTADO_MOROSOS = "  Listado de Morosos";
        public const string MENU_CONFIGURACION = "  Configuracion";
        public const string MENU_GESTION_USUARIOS = "  Gestión de Usuarios";
        public const string MENU_CERRAR_SESION = "  Cerrar Sesión";

        // --- MENSAJES DE CONFIRMACIÓN ---
        public const string PRINCIPAL_TITULO_CONFIRMAR_SALIDA = "Confirmación de Salida";
        public const string PRINCIPAL_MSG_CONFIRMAR_SALIDA = "¿Está seguro que desea salir del sistema y cerrar la aplicación?";
        public const string PRINCIPAL_TITULO_CONFIRMAR_CIERRE_SESION = "Confirmación de Cierre";
        public const string PRINCIPAL_MSG_CONFIRMAR_CIERRE_SESION = "¿Está seguro que desea cerrar la sesión actual?";


        // =================================================================
        //               BLOQUE 4: FrmInscripcionClub (INSCRIPCION)
        // =================================================================

        // --- TÍTULOS Y ETIQUETAS ---
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
        public const string INSCRIPCION_LBL_HORA_ACTUAL_PLACEHOLDER = "Hora Actual";
        public const string INSCRIPCION_CHK_APTO_FISICO_NO_SOCIO = "Apto Fisico Para No Socio";
        public const string INSCRIPCION_BTN_REGISTRAR_SOCIO = "REGISTRAR SOCIO Y PAGAR CUOTA N°1";
        public const string INSCRIPCION_BTN_REGISTRAR_NO_SOCIO = "REGISTRAR ACCESO Y COBRAR";
        public const string INSCRIPCION_BTN_CANCELAR = "CANCELAR / CERRAR";


        // --- MENSAJES ---
        public const string INSCRIPCION_ERROR_CARNET_CALCULO = "ERROR";
        public const string INSCRIPCION_ERROR_OBTENER_CARNET = "Error al obtener el número de carnet: {0}";
        public const string INSCRIPCION_TITULO_ERROR_INSCRIPCION = "Error de Inscripción";

        // Validaciones
        public const string INSCRIPCION_VALIDACION_CAMPOS_REQUERIDOS = "Los campos DNI, Nombre y Apellido son obligatorios.";
        public const string INSCRIPCION_TITULO_ADVERTENCIA_CAMPOS = "Advertencia de Campos Faltantes";
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
        public const string INSCRIPCION_SOCIO_REGISTRO_EXITO_MSG = "¡Socio {0} registrado y PRIMERA CUOTA COBRADA con éxito!\n\nID Persona: {1}\nNúmero de Carnet Asignado: {2}\nMonto Cobrado: ${3:N2} ({4})";
        public const string INSCRIPCION_SOCIO_REGISTRO_EXITO_TITULO = "Registro de Socio OK";
        public const string INSCRIPCION_SOCIO_ERROR_FORMATO_CARNET = "El Número de Carnet debe ser un valor numérico entero o el valor precalculado es incorrecto.";

        // No Socio - Flujo de Pago
        public const string INSCRIPCION_NO_SOCIO_PAGO_MSG = "Seleccione la forma de pago del ACCESO DIARIO (Monto: ${0:N2}):";
        public const string INSCRIPCION_NO_SOCIO_PAGO_ALERTA_FALTA = "Debe seleccionar la forma de pago para registrar el acceso diario.";
        public const string INSCRIPCION_NO_SOCIO_PAGO_CONFIRMACION_TITULO = "Confirmación de Pago y Registro de Acceso";
        public const string INSCRIPCION_NO_SOCIO_PAGO_CONFIRMACION_MSG = "Confirma el registro y acceso diario del No Socio, y el cobro de ${0:N2} (pago en {1})?";
        public const string INSCRIPCION_NO_SOCIO_REGISTRO_EXITO_MSG = "¡No Socio {0} registrado y acceso diario cobrado con éxito!\nID: {1}\nMonto cobrado: ${2:N2} en {3}.";
        public const string INSCRIPCION_NO_SOCIO_REGISTRO_EXITO_TITULO = "Registro de Acceso OK";
        public const string INSCRIPCION_NO_SOCIO_ERROR_ACCESO_TITULO = "Error de Registro de Acceso";
        public const string INSCRIPCION_NO_SOCIO_CONCEPTO_ACCESO = "Acceso Diario General";


        // =================================================================
        //               BLOQUE 5: FrmRegistrarPagos (PAGOS)
        // =================================================================

        // Búsqueda y Concepto
        public const string BUSQUEDA_FALLIDA_MSG = "No se encontró ninguna persona con ese identificador.";
        public const string CONCEPTO_PAGO_INVALIDO_ERROR_MSG = "Concepto de pago no válido. Contacte al administrador.";

        // Validaciones de Pago
        public const string PERSONA_NO_ENCONTRADA_WARN_MSG = "Debe buscar y seleccionar una persona válida.";
        public const string MONTO_INVALIDO_WARN_MSG = "El monto a pagar no es válido.";
        public const string FORMA_PAGO_INVALIDA_WARN_MSG = "Debe seleccionar una forma de pago.";

        // Resultado de Registro
        public const string REGISTRO_FALLO_ERROR_MSG = "Error al registrar el pago. Contacte al administrador. (Resultado 0/Falso)";
        public const string PAGO_ERROR_CRITICO_ERROR_MSG = "Error crítico al procesar el pago.";

        // Impresión
        public const string TITULO_IMPRESION = "Impresión de Comprobante";
        public const string IMPRESION_FALLO_WARN_MSG = "Advertencia de Impresión";
        public const string TITULO_REIMPRESION_EXITO = "Reimpresión Exitosa";
        public const string REIMPRESION_CARNET_WARN_MSG = "El carnet no puede ser reimpreso. Asegúrese de que el socio esté al día y tenga un número de carnet asignado.";
        public const string IMPRESION_ERROR_CRITICO_ERROR_MSG = "Error Crítico de Generación";


        // =================================================================
        //                         BLOQUE 6: PDF (GENERAL)
        // =================================================================

        // --- PDF (General) ---
        public const string PDF_CARNET_TITULO_EXITO = "Carnet Generado y Abierto";
        public const string PDF_CARNET_EXITO_MSG = "El Carnet de Socio ha sido generado y ABIERTO correctamente.\n\nRuta de guardado: {0}";
        public const string PDF_CARNET_ERROR_MSG = "Advertencia: El Carnet se generó, pero no se pudo abrir automáticamente.\nError: {0}";
        public const string PDF_COMPROBANTE_TITULO_EXITO = "Comprobante Generado";
        public const string PDF_COMPROBANTE_EXITO_MSG = "El Comprobante de Acceso ha sido generado y ABIERTO (o intentado abrir) correctamente.\n\nRuta de guardado: {0}";
        public const string PDF_COMPROBANTE_ERROR_MSG = "Advertencia: El Comprobante se generó, pero no se pudo abrir automáticamente.\nError: {0}";

        // =================================================================
        //               BLOQUE 7: FrmGestionUsuarios (USUARIOS)
        // =================================================================

        // --- TÍTULOS Y ETIQUETAS ---
        public const string USUARIOS_TITULO_FORM = "GESTIÓN DE USUARIOS";
        public const string USUARIOS_GB_DATOS = "Datos del Usuario";
        public const string USUARIOS_LBL_USUARIO = "Nombre Usuario:";
        public const string USUARIOS_LBL_CONTRASENA = "Contraseña:";
        public const string USUARIOS_LBL_CONFIRMAR_CONTRASENA = "Confirmar Contraseña:";
        public const string USUARIOS_LBL_ROL = "Seleccionar Rol:";

        // --- TEXTOS DE BOTONES ESPECÍFICOS ---
        public const string USUARIOS_BTN_REGISTRAR = "REGISTRAR";
        public const string USUARIOS_BTN_NUEVO = "NUEVO"; // Usado para limpiar/cancelar edición
        public const string USUARIOS_BTN_EDITAR = "EDITAR";
        public const string USUARIOS_BTN_ELIMINAR = "ELIMINAR";
        public const string USUARIOS_BTN_CERRAR = "CERRAR";

        // --- MENSAJES DE VALIDACIÓN Y PROCESO ---
        public const string USUARIOS_MSG_CAMPOS_OBLIGATORIOS = "Debe completar todos los campos.";
        public const string USUARIOS_MSG_CONTRASENAS_NO_COINCIDEN = "Las contraseñas no coinciden. Por favor, verifique.";
        public const string USUARIOS_MSG_SELECCION_ROL_OBLIGATORIA = "Debe seleccionar un rol.";
        public const string USUARIOS_MSG_USUARIO_SELECCION_OBLIGATORIA = "Debe seleccionar un usuario de la lista.";
        public const string USUARIOS_MSG_CONFIRMAR_ELIMINAR = "¿Está seguro que desea eliminar este usuario?";

        // --- MENSAJES DE RESULTADO ---
        public const string USUARIOS_MSG_REGISTRO_EXITO = "Usuario registrado con éxito.";
        public const string USUARIOS_MSG_REGISTRO_ERROR = "Error al registrar el usuario: ";
        public const string USUARIOS_MSG_MODIFICACION_EXITO = "Usuario modificado correctamente.";
        public const string USUARIOS_MSG_MODIFICACION_ERROR = "Error al modificar el usuario: ";
        public const string USUARIOS_MSG_ELIMINACION_EXITO = "Usuario eliminado correctamente.";
        public const string USUARIOS_MSG_ELIMINACION_ERROR = "Error al eliminar el usuario: ";



    }
}