using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace ClubDeportivo.UI.DAL
{
    // Clase DAL específica para la vista de control de cuotas
    public class SocioEstadoCuotaDAL
    {
        /// <summary>
        /// Obtiene un listado de todos los socios activos que tienen CUOTAS PENDIENTES.
        /// Consulta directamente la vista SQL 'v_socios_estado_cuota'.
        /// </summary>
        /// <returns>DataTable con la lista de socios y su estado de cuota.</returns>
        public DataTable ObtenerListadoMaestroCuotas()
        {
            MySqlConnection sqlCon = null;
            DataTable dtResultado = new DataTable();

            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion();

                // CRÍTICO: Consulta a la VIEW y filtramos en DAL solo a aquellos con cuotas pendientes.
                // Si 'id_cuota_pendiente' es NULL, el socio está completamente al día.
                MySqlCommand comando = new MySqlCommand(
                    "select * from v_socios_estado_cuota",
                    sqlCon
                );
                comando.CommandType = CommandType.Text;

                sqlCon.Open();

                // Usamos DataAdapter para llenar el DataTable
                MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
                adaptador.Fill(dtResultado);
            }
            catch (Exception ex)
            {
                // Incluimos un mensaje claro sobre el error de la vista/conexión
                throw new Exception("Error DAL al obtener el listado maestro de cuotas (VIEW): " + ex.Message);
            }
            finally
            {
                if (sqlCon != null && sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }

            return dtResultado;
        }

        // Aquí se agregarían otros métodos DAL para CUOTAS si fueran necesarios (ej: RegistrarPago)
    }
}