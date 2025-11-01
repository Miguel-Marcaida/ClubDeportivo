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
    public class SocioDAL
    {
        // El método inserta los datos específicos del socio.
        // Asumimos que la Persona ya fue registrada y el 'id_persona' está cargado en el objeto 'Socio'.
        public void RegistrarSocio(Socio oSocio)
        {
            MySqlConnection conn = null;

            try
            {
                conn = Conexion.getInstancia().CrearConexion();

                // CRÍTICO: Llamamos al Stored Procedure 'RegistrarSocio'
                MySqlCommand comando = new MySqlCommand("RegistrarSocio", conn);
                comando.CommandType = CommandType.StoredProcedure;

                // Parámetros: deben coincidir con los del SP
                comando.Parameters.Add("p_id_persona", MySqlDbType.Int32).Value = oSocio.IdPersona;
                comando.Parameters.Add("p_numero_carnet", MySqlDbType.Int32).Value = oSocio.NumeroCarnet;
                comando.Parameters.Add("p_estado_activo", MySqlDbType.Byte).Value = oSocio.EstadoActivo; // Usamos Byte para BOOLEAN en MySql
                comando.Parameters.Add("p_ficha_medica_entregada", MySqlDbType.Byte).Value = oSocio.FichaMedicaEntregada;

                conn.Open();
                comando.ExecuteNonQuery();

                // No necesitamos devolver nada, el registro es exitoso si no hay excepción.
            }
            catch (Exception ex)
            {
                // Devolvemos el error a la BLL
                throw new Exception("Error en DAL al registrar el Socio: " + ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open) { conn.Close(); }
            }
        }



        /// <summary>
        /// Cuenta el número total de registros en la tabla 'socios'.
        /// </summary>
        public int ContarSocios()
        {
            MySqlConnection sqlCon = null;
            int totalSocios = 0;
            string consulta = "SELECT COUNT(*) FROM socios;";

            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion();
                MySqlCommand comando = new MySqlCommand(consulta, sqlCon);
                sqlCon.Open();

                // ExecuteScalar es ideal para obtener un valor único como un COUNT
                object resultado = comando.ExecuteScalar();

                if (resultado != null && resultado != DBNull.Value)
                {
                    // Convertimos el resultado del conteo a entero
                    totalSocios = Convert.ToInt32(resultado);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error DAL al contar registros de socios: " + ex.Message);
            }
            finally
            {
                if (sqlCon != null && sqlCon.State == System.Data.ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
            return totalSocios;
        }


    }
}
