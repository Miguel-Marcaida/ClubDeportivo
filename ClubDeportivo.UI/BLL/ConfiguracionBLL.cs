using ClubDeportivo.UI.DAL;
using ClubDeportivo.UI.Entidades;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubDeportivo.UI.BLL
{
    public class ConfiguracionBLL
    {
        private readonly ConfiguracionDAL oConfiguracionDAL = new ConfiguracionDAL();

        // -----------------------------------------------------------
        // LÓGICA DE LECTURA (Tus métodos originales)
        // -----------------------------------------------------------
        /// <summary>
        /// Obtiene el monto base de la cuota mensual desde la configuración global.
        /// </summary>
        /// <returns>Monto de la cuota como DECIMAL.</returns>
        public decimal ObtenerMontoCuotaBase()
        {
            // Clave que está definida en el script de MySQL
            const string CLAVE_MONTO = "CUOTA_MENSUAL_BASE";

            string valorString = oConfiguracionDAL.ObtenerValorPorClave(CLAVE_MONTO);

            if (string.IsNullOrEmpty(valorString))
            {
                // CRÍTICO: Si no está en la BD, lanzamos un error de configuración
                throw new Exception($"Error de configuración: No se encontró la clave '{CLAVE_MONTO}' en la base de datos.");
            }

            // LÓGICA CRÍTICA: FORZAR CULTURA INVARIANTE (usa siempre el punto '.' como decimal)
            if (decimal.TryParse(valorString, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal monto))
            {
                return monto;
            }
            else
            {
                throw new Exception($"Error de formato: El valor de '{CLAVE_MONTO}' ('{valorString}') en la base de datos no es un número decimal válido.");
            }
        }


        /// <summary>
        /// Obtiene el monto base del acceso diario desde la configuración global.
        /// </summary>
        /// <returns>Monto del acceso diario como DECIMAL.</returns>
        public decimal ObtenerMontoAccesoDiario()
        {
            // Clave que está definida en el script de MySQL
            const string CLAVE_MONTO = "ACCESO_DIARIO_BASE";

            string valorString = oConfiguracionDAL.ObtenerValorPorClave(CLAVE_MONTO);

            if (string.IsNullOrEmpty(valorString))
            {
                // CRÍTICO: Si no está en la BD, lanzamos un error de configuración
                throw new Exception($"Error de configuración: No se encontró la clave '{CLAVE_MONTO}' en la base de datos. Agregue la clave 'ACCESO_DIARIO_BASE'.");
            }

            // LÓGICA CRÍTICA: FORZAR CULTURA INVARIANTE (usa siempre el punto '.' como decimal)
            if (decimal.TryParse(valorString, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal monto))
            {
                return monto;
            }
            else
            {
                throw new Exception($"Error de formato: El valor de '{CLAVE_MONTO}' ('{valorString}') en la base de datos no es un número decimal válido.");
            }
        }

        // -----------------------------------------------------------
        // LÓGICA DE ABM (Nuevos métodos)
        // -----------------------------------------------------------

        /// <summary>
        /// Obtiene todas las configuraciones para llenar la grilla.
        /// </summary>
        public List<ConfiguracionGlobal> ListarTodos()
        {
            // La BLL solo actúa como pasarela para la lectura de datos, sin lógica adicional.
            return oConfiguracionDAL.ObtenerTodas();
        }

        /// <summary>
        /// Guarda o modifica una configuración global, aplicando validaciones de negocio.
        /// </summary>
        public bool GuardarOModificar(ConfiguracionGlobal config)
        {
            // --- 1. VALIDACIÓN DE NEGOCIO ---
            if (string.IsNullOrWhiteSpace(config.Clave))
            {
                throw new Exception("La CLAVE de configuración no puede estar vacía.");
            }
            if (string.IsNullOrWhiteSpace(config.Valor))
            {
                throw new Exception("El VALOR de configuración no puede estar vacío.");
            }
            if (config.Clave.Length > 100)
            {
                throw new Exception("La CLAVE no puede superar los 100 caracteres.");
            }
            if (config.Valor.Length > 255)
            {
                throw new Exception("El VALOR no puede superar los 255 caracteres.");
            }

            // Opcional: Estandarizar la clave a MAYÚSCULAS para consistencia.
            config.Clave = config.Clave.ToUpper().Trim();

            // --- 2. LLAMADA AL DAL ---
            // El DAL se encarga de manejar si es un INSERT o un UPDATE.
            return oConfiguracionDAL.GuardarOModificar(config);
        }


        /// <summary>
        /// Elimina una configuración por su ID.
        /// </summary>
        public bool Eliminar(int idConfig)
        {
            if (idConfig <= 0)
            {
                throw new Exception("Debe seleccionar una configuración válida para eliminar.");
            }

            // Opcional: Se podría añadir lógica para prohibir la eliminación de claves críticas (ej: CUOTA_MENSUAL_BASE)
            // if (idConfig es una clave critica) { throw new Exception("Esta configuración es crítica y no puede eliminarse."); }

            return oConfiguracionDAL.Eliminar(idConfig);
        }


    }
}
