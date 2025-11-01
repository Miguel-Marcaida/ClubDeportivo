using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClubDeportivo.UI.Utilitarios
{

    /// <summary>
    /// Clase estática para encapsular lógica común de filtrado de entrada (KeyPress) y validación de formato (Guardar/Validating).
    /// </summary>
    public static class Validaciones
    {
        // ====================================================================
        // MÉTODOS DE FILTRADO DE ENTRADA (KeyPress Event)
        // Se llaman en el evento KeyPress de un TextBox para restringir caracteres.
        // ====================================================================

        /// <summary>
        /// Permite solo la entrada de dígitos (0-9) y teclas de control (Backspace, Enter, etc.).
        /// Ideal para campos de DNI, CUIL, o Teléfono.
        /// </summary>
        public static void SoloNumeros(KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Ignora la tecla presionada
            }
        }

        /// <summary>
        /// Permite la entrada de letras (mayúsculas/minúsculas), espacios y teclas de control.
        /// Ideal para campos de Nombre y Apellido.
        /// </summary>
        public static void SoloLetras(KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // Ignora la tecla presionada
            }
        }

        /// <summary>
        /// Permite solo la entrada de letras, números, espacios y teclas de control.
        /// Excluye símbolos y caracteres especiales.
        /// </summary>
        public static void SoloAlfanumericos(KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) &&
                !char.IsControl(e.KeyChar) &&
                !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // Ignora la tecla presionada si es un símbolo o caracter especial
            }
        }

        /// <summary>
        /// Convierte la tecla presionada a su equivalente en mayúscula.
        /// </summary>
        public static void ForzarMayusculas(KeyPressEventArgs e)
        {
            if (char.IsLower(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
        }


        // ====================================================================
        // MÉTODOS DE VALIDACIÓN DE FORMATO (Validating Event o al Guardar)
        // Se llaman al intentar guardar o validar el contenido final del campo.
        // ====================================================================

        /// <summary>
        /// Verifica si la cadena no está vacía o solo contiene espacios. (Usado para campos obligatorios).
        /// </summary>
        public static bool EsTextoRequerido(string texto)
        {
            return !string.IsNullOrWhiteSpace(texto);
        }

        /// <summary>
        /// Comprueba si un texto representa un número entero largo (long).
        /// Si el texto está vacío, retorna True (asumiendo que la obligatoriedad se comprueba con EsTextoRequerido).
        /// </summary>
        public static bool EsNumerico(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return true;
            }
            return long.TryParse(texto, out _);
        }

        /// <summary>
        /// Comprueba si un texto tiene un formato de correo electrónico básico (contiene @ y .).
        /// Si el email está vacío, retorna True (asumiendo que es opcional).
        /// </summary>
        public static bool EsFormatoEmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return true; // Si es opcional, lo permitimos vacío.
            }

            // Expresión regular simple para validar formato básico: a@b.c
            string patronEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, patronEmail);
        }

        // Alias para compatibilidad con nombres anteriores
        public static bool EsCampoObligatorio(string texto) => EsTextoRequerido(texto);
        public static bool EsEmailValido(string email) => EsFormatoEmailValido(email);
    }

}
