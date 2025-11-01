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
    public class ConfiguracionDAL
    {
        // -----------------------------------------------------------
        // LECTURA SIMPLE (MÉTODO QUE ENVIASTE)
        // -----------------------------------------------------------
        public string ObtenerValorPorClave(string clave)
        {
            MySqlConnection sqlCon = null;
            MySqlDataReader resultado = null;
            string valor = null;

            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion();

                // Usamos una consulta directa ya que es una tabla de catálogo simple.
                // Si la tabla fuera consultada muy frecuentemente, se podría usar un SP.
                MySqlCommand comando = new MySqlCommand("SELECT valor FROM configuraciones_globales WHERE clave = @clave", sqlCon);

                comando.Parameters.AddWithValue("@clave", clave);

                sqlCon.Open();
                resultado = comando.ExecuteReader();

                if (resultado.Read())
                {
                    // El valor es VARCHAR en la DB, lo leemos como string.
                    valor = resultado["valor"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error DAL al obtener configuración: " + ex.Message);
            }
            finally
            {
                if (sqlCon != null && sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
            return valor;
        }


        // -----------------------------------------------------------
        // OBTENER TODAS (NECESARIO PARA LA GRILLA DE ABM)
        // -----------------------------------------------------------
        public List<ConfiguracionGlobal> ObtenerTodas()
        {
            List<ConfiguracionGlobal> lista = new List<ConfiguracionGlobal>();
            MySqlConnection sqlCon = null;
            MySqlDataReader reader = null;
            string query = "SELECT id_config, clave, valor, descripcion FROM configuraciones_globales";

            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion();
                sqlCon.Open();
                MySqlCommand cmd = new MySqlCommand(query, sqlCon);

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ConfiguracionGlobal config = new ConfiguracionGlobal
                    {
                        IdConfig = reader.GetInt32("id_config"),
                        Clave = reader.GetString("clave"),
                        Valor = reader.GetString("valor"),
                        // Manejo de valores NULL en la columna 'descripcion'
                        Descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion")) ? string.Empty : reader.GetString("descripcion")
                    };
                    lista.Add(config);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error DAL al obtener todas las configuraciones: " + ex.Message);
            }
            finally
            {
                if (sqlCon != null && sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
            return lista;
        }

        // -----------------------------------------------------------
        // GUARDAR (INSERTAR O ACTUALIZAR)
        // -----------------------------------------------------------
        public bool GuardarOModificar(ConfiguracionGlobal config)
        {
            MySqlConnection sqlCon = null;
            string query = "";
            int filasAfectadas = 0;

            // Define la query según sea INSERT o UPDATE
            if (config.IdConfig > 0)
            {
                query = @"UPDATE configuraciones_globales SET 
                            clave = @clave, 
                            valor = @valor, 
                            descripcion = @descripcion 
                          WHERE id_config = @idConfig";
            }
            else
            {
                query = @"INSERT INTO configuraciones_globales (clave, valor, descripcion) 
                          VALUES (@clave, @valor, @descripcion)";
            }

            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion();
                sqlCon.Open();
                MySqlCommand cmd = new MySqlCommand(query, sqlCon);

                cmd.Parameters.AddWithValue("@clave", config.Clave);
                cmd.Parameters.AddWithValue("@valor", config.Valor);
                // Si la descripción es nula o vacía, enviamos DBNull para la columna NULLABLE
                cmd.Parameters.AddWithValue("@descripcion", string.IsNullOrEmpty(config.Descripcion) ? (object)DBNull.Value : config.Descripcion);

                if (config.IdConfig > 0)
                {
                    cmd.Parameters.AddWithValue("@idConfig", config.IdConfig);
                }

                filasAfectadas = cmd.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
            catch (MySqlException ex)
            {
                // Manejar la excepción de clave duplicada (código 1062 en MySQL)
                if (ex.Number == 1062)
                {
                    throw new Exception("Ya existe una configuración con la misma CLAVE. Por favor, ingrese una clave única.");
                }
                throw new Exception("Error DAL al guardar la configuración: " + ex.Message);
            }
            finally
            {
                if (sqlCon != null && sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }

        // -----------------------------------------------------------
        // ELIMINAR
        // -----------------------------------------------------------
        public bool Eliminar(int idConfig)
        {
            MySqlConnection sqlCon = null;
            string query = "DELETE FROM configuraciones_globales WHERE id_config = @idConfig";

            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion();
                sqlCon.Open();
                MySqlCommand cmd = new MySqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@idConfig", idConfig);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error DAL al eliminar la configuración: " + ex.Message);
            }
            finally
            {
                if (sqlCon != null && sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }


    }
}
