using ClubDeportivo.UI.Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubDeportivo.UI.DAL
{
    public class NoSocioDAL
    {
        // El método inserta los datos específicos del no socio.
        public void RegistrarNoSocio(NoSocio oNoSocio)
        {
            MySqlConnection conn = null;

            try
            {
                conn = Conexion.getInstancia().CrearConexion();

                // CRÍTICO: Llamamos al Stored Procedure 'RegistrarNoSocio'
                MySqlCommand comando = new MySqlCommand("RegistrarNoSocio", conn);
                comando.CommandType = CommandType.StoredProcedure;

                // Parámetros: deben coincidir con los del SP
                comando.Parameters.Add("p_id_persona", MySqlDbType.Int32).Value = oNoSocio.IdPersona;
                // Manejamos el valor nullable (DateTime?) para FechaPagoDia
                comando.Parameters.Add("p_fecha_pago_dia", MySqlDbType.Date).Value =
                    oNoSocio.FechaPagoDia.HasValue ? (object)oNoSocio.FechaPagoDia.Value : DBNull.Value;

                conn.Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception("Error en DAL al registrar el No Socio: " + ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open) { conn.Close(); }
            }
        }
    }
}
