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
    public class RegistroAccesoDAL
    {
        public int RegistrarAcceso(RegistroAcceso oRegistro)
        {
            MySqlConnection sqlCon = null;
            int idGenerado = 0;

            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion();

                // Llama al Stored Procedure 'RegistrarAccesoDiario' que crearemos en el último paso.
                MySqlCommand comando = new MySqlCommand("RegistrarAccesoDiario", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;

                // Parámetros de ENTRADA (IN)
                comando.Parameters.Add("p_id_persona", MySqlDbType.Int32).Value = oRegistro.IdPersona;
                comando.Parameters.Add("p_monto", MySqlDbType.Decimal).Value = oRegistro.Monto;
                comando.Parameters.Add("p_fecha", MySqlDbType.DateTime).Value = oRegistro.Fecha;
                comando.Parameters.Add("p_forma_pago", MySqlDbType.VarChar).Value = oRegistro.FormaPago;
                comando.Parameters.Add("p_id_actividad", MySqlDbType.Int32).Value = oRegistro.IdActividad;

                // Parámetro de SALIDA (OUT)
                MySqlParameter pIdGenerado = new MySqlParameter("p_id_generado", MySqlDbType.Int32);
                pIdGenerado.Direction = ParameterDirection.Output;
                comando.Parameters.Add(pIdGenerado);

                sqlCon.Open();
                comando.ExecuteNonQuery();

                // Leer el valor del parámetro de SALIDA
                idGenerado = Convert.ToInt32(pIdGenerado.Value);
            }
            catch (Exception ex)
            {
                throw new Exception("Error DAL al registrar el acceso diario: " + ex.Message);
            }
            finally
            {
                if (sqlCon != null && sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }

            return idGenerado;
        }
    }
}
