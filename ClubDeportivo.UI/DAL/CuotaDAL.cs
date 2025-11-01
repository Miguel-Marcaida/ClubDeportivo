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
    public class CuotaDAL
    {
        // 1. Método para registrar el pago de una cuota
        // Recibe el objeto Cuota (que debe tener todos sus campos rellenados, excepto IdCuota)
        public int RegistrarPagoCuota(Cuota oCuota)
        {
            MySqlConnection sqlCon = null;
            int idCuotaGenerada = 0;

            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion();
                MySqlCommand comando = new MySqlCommand("RegistrarPagoCuota", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;

                // Parámetros de ENTRADA (IN)
                comando.Parameters.Add("p_id_persona", MySqlDbType.Int32).Value = oCuota.IdPersona;
                comando.Parameters.Add("p_monto", MySqlDbType.Decimal).Value = oCuota.Monto;
                comando.Parameters.Add("p_fecha_vencimiento", MySqlDbType.Date).Value = oCuota.FechaVencimiento;

                // CRÍTICO: Usamos .Value del Nullable, pero como es un pago, asumimos que tiene valor.
                comando.Parameters.Add("p_fecha_pago", MySqlDbType.Date).Value = oCuota.FechaPago.Value;
                comando.Parameters.Add("p_forma_pago", MySqlDbType.VarChar).Value = oCuota.FormaPago;

                // <<<< LÍNEA CRÍTICA AÑADIDA >>>>
                comando.Parameters.Add("p_concepto", MySqlDbType.VarChar).Value = oCuota.Concepto;


                // Parámetro de SALIDA (OUT)
                MySqlParameter pIdGenerado = new MySqlParameter("p_id_cuota_generada", MySqlDbType.Int32);
                pIdGenerado.Direction = ParameterDirection.Output;
                comando.Parameters.Add(pIdGenerado);

                sqlCon.Open();
                comando.ExecuteNonQuery(); // Ejecuta el SP

                // Leer el valor del parámetro de SALIDA
                idCuotaGenerada = Convert.ToInt32(pIdGenerado.Value);
            }
            catch (Exception ex)
            {
                // Devolvemos 0 si hay un error o lanzamos una excepción más descriptiva
                throw new Exception("Error DAL al registrar el pago de cuota: " + ex.Message);
            }
            finally
            {
                if (sqlCon != null && sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }

            return idCuotaGenerada;
        }

        // PENDIENTE: Método para ObtenerUltimaCuotaPagada(int idPersona)
        // PENDIENTE: Método para ListarCuotasImpagas(int idPersona)
    }
}
