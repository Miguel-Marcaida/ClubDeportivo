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
    public class PersonaDAL
    {
        // 1. Método para verificar si el DNI ya existe, usando Stored Procedure
        // Retorna IdPersona si existe (mayor a 0), o 0 si no existe.
        public int ObtenerIdPersonaPorDni(string dni)
        {
            int idPersona = 0;
            MySqlConnection conn = null;
            MySqlDataReader resultado = null; // Necesario para leer la respuesta del SELECT

            try
            {
                conn = Conexion.getInstancia().CrearConexion();

                // CRÍTICO: Llamada al Stored Procedure 'ObtenerIdPersonaPorDni'
                MySqlCommand comando = new MySqlCommand("ObtenerIdPersonaPorDni", conn);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_dni", MySqlDbType.VarChar).Value = dni;

                conn.Open();
                resultado = comando.ExecuteReader();

                if (resultado.Read()) // Si se encontró una fila
                {
                    // Leemos el resultado (la columna se llama 'id_persona')
                    idPersona = resultado.GetInt32("id_persona");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en DAL al buscar DNI (SP): " + ex.Message);
            }
            finally
            {
                if (resultado != null) { resultado.Close(); }
                if (conn != null && conn.State == ConnectionState.Open) { conn.Close(); }
            }

            return idPersona;
        }

        // 2. Método para insertar una nueva persona usando el STORED PROCEDURE 'RegistrarPersona'
        // Retorna el IdPersona generado (PK).
        public int RegistrarPersona(Persona p)
        {
            int idGenerado = 0;
            MySqlConnection conn = null;

            try
            {
                conn = Conexion.getInstancia().CrearConexion();

                // Llamada al Stored Procedure 'RegistrarPersona'
                MySqlCommand comando = new MySqlCommand("RegistrarPersona", conn);
                comando.CommandType = CommandType.StoredProcedure;

                // Parámetros de ENTRADA
                comando.Parameters.Add("p_dni", MySqlDbType.VarChar).Value = p.Dni;
                comando.Parameters.Add("p_nombre", MySqlDbType.VarChar).Value = p.Nombre;
                comando.Parameters.Add("p_apellido", MySqlDbType.VarChar).Value = p.Apellido;
                comando.Parameters.Add("p_fecha_nacimiento", MySqlDbType.Date).Value = p.FechaNacimiento.HasValue ? (object)p.FechaNacimiento.Value : DBNull.Value;
                comando.Parameters.Add("p_telefono", MySqlDbType.VarChar).Value = p.Telefono;
                comando.Parameters.Add("p_email", MySqlDbType.VarChar).Value = p.Email;

                // Parámetro de SALIDA
                comando.Parameters.Add("p_id_generado", MySqlDbType.Int32).Direction = ParameterDirection.Output;

                conn.Open();
                comando.ExecuteNonQuery();

                // Recuperamos el ID generado del parámetro de salida
                idGenerado = Convert.ToInt32(comando.Parameters["p_id_generado"].Value);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en DAL al registrar la persona (SP): " + ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open) { conn.Close(); }
            }

            return idGenerado;
        }


        /// <summary>
        /// OBTENER DETALLE PARA REGISTRO DE PAGO (NUEVO MÉTODO)
        /// Busca la persona por DNI o Carnet y obtiene los detalles de pago necesarios.
        /// </summary>
        /// <param name="identificador">DNI o Número de Carnet.</param>
        /// <returns>PersonaPagoDetalleDTO o null si no se encuentra.</returns>
        public PersonaPagoDetalleDTO BuscarPersonaDetalle(string identificador)
        {
            PersonaPagoDetalleDTO detalle = null;
            MySqlConnection conn = null;
            MySqlDataReader resultado = null;

            try
            {
                conn = Conexion.getInstancia().CrearConexion();

                // Llamada al nuevo Stored Procedure 'BuscarPersonaParaPagoSP'
                MySqlCommand comando = new MySqlCommand("BuscarPersonaParaPagoSP", conn);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_identificador", MySqlDbType.VarChar).Value = identificador;

                conn.Open();
                resultado = comando.ExecuteReader();

                if (resultado.Read())
                {
                    detalle = new PersonaPagoDetalleDTO
                    {
                        // Mapeo directo de columnas:
                        IdPersona = resultado.GetInt32("id_persona"),
                        DNI = resultado.GetString("dni"),
                        Nombre = resultado.GetString("nombre"),
                        Apellido = resultado.GetString("apellido"),
                        EsSocio = resultado.GetBoolean("es_socio")

                        // NOTA CLAVE: La columna ultima_cuota_cubierta puede ser NULL en MySQL.
                        // Debemos mapear NULL a DateTime.MinValue, que la BLL interpreta como 'sin pagos'.
                    };

                    // 🚨 Mapeo del NUEVO CAMPO NumeroCarnet (int?)
                    if (resultado.IsDBNull(resultado.GetOrdinal("numero_carnet")))
                    {
                        detalle.NumeroCarnet = null; // Asignar null al int? de C#
                    }
                    else
                    {
                        detalle.NumeroCarnet = resultado.GetInt32("numero_carnet");
                    }

                    if (resultado.IsDBNull(resultado.GetOrdinal("ultima_cuota_cubierta")))
                    {
                        detalle.UltimaCuotaCubierta = DateTime.MinValue;
                    }
                    else
                    {
                        detalle.UltimaCuotaCubierta = resultado.GetDateTime("ultima_cuota_cubierta");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error DAL al buscar el detalle de pago (SP): " + ex.Message);
            }
            finally
            {
                if (resultado != null) { resultado.Close(); }
                if (conn != null && conn.State == ConnectionState.Open) { conn.Close(); }
            }

            return detalle;
        }

        /// <summary>
        /// Obtiene un listado de todas las personas vigentes (Socio y No Socio)
        /// desde la vista unificada de MySQL, listo para ser mapeado a DTOs.
        /// </summary>
        public DataTable ObtenerListadoUnificado()
        {
            MySqlConnection sqlCon = null;
            DataTable dtResultado = new DataTable();

            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion();

                // 1. Usamos la VIEW que creaste, que ya filtra por EstaVigente = 1
                MySqlCommand comando = new MySqlCommand("SELECT * FROM v_listado_personas", sqlCon);
                comando.CommandType = CommandType.Text;

                sqlCon.Open();

                // 2. Ejecutar y llenar el DataTable
                MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
                adaptador.Fill(dtResultado);
            }
            catch (Exception ex)
            {
                // Incluimos un mensaje claro en caso de que la vista no exista o haya un error de conexión
                throw new Exception("Error DAL al obtener el listado unificado de personas (VIEW): " + ex.Message);
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

        // Dentro de la clase PersonaDAL
        public bool DarDeBajaPersonaLogica(int idPersona)
        {
            MySqlConnection sqlCon = null;
            bool resultado = false;

            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion();

                MySqlCommand comando = new MySqlCommand("DarDeBajaPersonaLogica", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;

                // Parámetro de entrada
                comando.Parameters.AddWithValue("@p_id_persona", idPersona);

                sqlCon.Open();

                // Si la ejecución es exitosa (sin excepción), y afecta al menos una fila
                if (comando.ExecuteNonQuery() > 0)
                {
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error DAL al realizar la baja lógica (Soft Delete): " + ex.Message);
            }
            finally
            {
                if (sqlCon != null && sqlCon.State == System.Data.ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }

            return resultado;
        }



        /// <summary>
        /// Obtiene el detalle completo de una persona (Socio o No Socio) para edición.
        /// </summary>
        public DataTable ObtenerDetallePersonaSP(int idPersona)
        {
            MySqlConnection sqlCon = null;
            DataTable dtResultado = new DataTable();

            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion();

                MySqlCommand comando = new MySqlCommand("ObtenerDetallePersona", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;

                // Parámetro de entrada
                comando.Parameters.AddWithValue("@p_id_persona", idPersona);

                sqlCon.Open();

                // Ejecutar y llenar el DataTable
                MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
                adaptador.Fill(dtResultado);
            }
            catch (Exception ex)
            {
                throw new Exception("Error DAL al obtener el detalle de la persona (SP): " + ex.Message);
            }
            finally
            {
                if (sqlCon != null && sqlCon.State == System.Data.ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }

            return dtResultado;
        }


        public int VerificarDniExistente(string dni, int idPersonaExcluir)
        {
            MySqlConnection sqlCon = null;
            int contador = 0; // Por defecto, 0 (no encontrado)

            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion();
                MySqlCommand comando = new MySqlCommand("VerificarDniExistente", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;

                // Parámetros de entrada
                comando.Parameters.Add("p_dni", MySqlDbType.VarChar).Value = dni;
                comando.Parameters.Add("p_id_persona_excluir", MySqlDbType.Int32).Value = idPersonaExcluir;

                sqlCon.Open();

                // Ejecutamos y leemos el resultado (un único valor entero)
                // Usamos ExecuteScalar ya que el SP devuelve un COUNT(id_persona)
                object resultado = comando.ExecuteScalar();

                if (resultado != null && resultado != DBNull.Value)
                {
                    contador = Convert.ToInt32(resultado);
                }
            }
            catch (Exception ex)
            {
                // En caso de error de conexión o SQL, asumimos que no hay duplicados y lanzamos excepción crítica.
                // La BLL capturará esto.
                throw new Exception("Error DAL al verificar DNI: " + ex.Message);
            }
            finally
            {
                if (sqlCon != null && sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
            return contador;
        }


        public string ActualizarDatosPersonaSP(PersonaDetalleDTO detalle)
        {
            MySqlConnection sqlCon = null;
            string respuesta = "";

            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion();
                MySqlCommand comando = new MySqlCommand("ActualizarDatosPersona", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;

                // Parámetros de entrada (coinciden con el SP que ya confirmamos)
                comando.Parameters.Add("p_id_persona", MySqlDbType.Int32).Value = detalle.IdPersona;
                comando.Parameters.Add("p_dni", MySqlDbType.VarChar).Value = detalle.Dni.Trim();
                comando.Parameters.Add("p_nombre", MySqlDbType.VarChar).Value = detalle.Nombre.Trim();
                comando.Parameters.Add("p_apellido", MySqlDbType.VarChar).Value = detalle.Apellido.Trim();
                comando.Parameters.Add("p_telefono", MySqlDbType.VarChar).Value = detalle.Telefono.Trim();
                comando.Parameters.Add("p_email", MySqlDbType.VarChar).Value = detalle.Email.Trim();

                // Manejo de Fecha de Nacimiento (NULL en MySQL si el DTO lo permite)
                if (detalle.FechaNacimiento.HasValue)
                {
                    comando.Parameters.Add("p_fecha_nacimiento", MySqlDbType.Date).Value = detalle.FechaNacimiento.Value;
                }
                else
                {
                    comando.Parameters.Add("p_fecha_nacimiento", MySqlDbType.Date).Value = DBNull.Value;
                }

                sqlCon.Open();

                int filasAfectadas = comando.ExecuteNonQuery();

                if (filasAfectadas >= 1)
                {
                    respuesta = "OK";
                }
                else
                {
                    respuesta = "No se modificó ningún registro, verifique si el ID existe.";
                }
            }
            catch (Exception ex)
            {
                respuesta = "Error de SQL al intentar actualizar: " + ex.Message;
            }
            finally
            {
                if (sqlCon != null && sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
            return respuesta;
        }



    }
}
