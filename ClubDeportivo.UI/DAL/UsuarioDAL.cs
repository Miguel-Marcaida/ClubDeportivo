using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ClubDeportivo.UI.Entidades;
using MySql.Data.MySqlClient;

namespace ClubDeportivo.UI.DAL
{
    public class UsuarioDAL
    {
        // 1. Método para obtener el hash de un usuario (para el login)
        public Usuario ValidarLogin(string usuario)
        {
            MySqlConnection sqlCon = null;
            Usuario oUsuario = null; // Objeto que contendrá el hash
            MySqlDataReader resultado = null;

            try
            {
                // Usa la conexión Singleton de la DAL
                sqlCon = Conexion.getInstancia().CrearConexion();

                // Llama al Stored Procedure 'IngresoLogin' que ya tienes creado en MySQL
                MySqlCommand comando = new MySqlCommand("IngresoLogin", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("p_usuario", MySqlDbType.VarChar).Value = usuario;

                sqlCon.Open();
                resultado = comando.ExecuteReader();

                if (resultado.Read())
                {
                    oUsuario = new Usuario
                    {
                        IdUsuario = Convert.ToInt32(resultado["IdUsuario"]),         // Lee el alias
                        NombreUsuario = Convert.ToString(resultado["NombreUsuario"]), // Lee el alias
                        ContrasenaHash = Convert.ToString(resultado["ContrasenaHash"]), // Lee el alias
                        Rol = Convert.ToString(resultado["Rol"])                       // Lee el alias
                    };
                }
            }
            catch (Exception ex)
            {
                // Lanza la excepción para que la BLL pueda decidir si es un error fatal.
                throw new Exception("Error en la capa de datos al validar login: " + ex.Message);
            }
            finally
            {
                if (resultado != null && !resultado.IsClosed) resultado.Close();
                if (sqlCon.State == ConnectionState.Open) { sqlCon.Close(); }
            }
            return oUsuario;
        }

        // 2. Método para insertar un nuevo usuario (para el registro)
        public string InsertarUsuario(Usuario user)
        {
            MySqlConnection sqlCon = null;
            string respuesta = "";

            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion();
                MySqlCommand comando = new MySqlCommand("RegistrarUsuario", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;

                // Parámetros: Los nombres deben coincidir con los del SP en MySQL
                comando.Parameters.Add("p_nombre_usuario", MySqlDbType.VarChar).Value = user.NombreUsuario;
                comando.Parameters.Add("p_contrasena_hash", MySqlDbType.VarChar).Value = user.ContrasenaHash;
                comando.Parameters.Add("p_rol", MySqlDbType.VarChar).Value = user.Rol; //CAMBIO

                sqlCon.Open();

                int filasAfectadas = comando.ExecuteNonQuery();

                if (filasAfectadas >= 1)
                {
                    respuesta = "OK";
                }
                else
                {
                    respuesta = "No se pudo insertar el registro.";
                }
            }
            catch (Exception ex)
            {
                // Capturamos cualquier error de SQL (ej. duplicación de nombre de usuario)
                respuesta = "Error de SQL: " + ex.Message;
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


        //Método para listar los usuarios d la tabla usuarios
        public DataTable ListarUsuarios()
        {
            MySqlConnection sqlCon = null;
            DataTable tabla = new DataTable();
            
            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion();
                MySqlCommand comando = new MySqlCommand("ListarUsuarios", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;

                sqlCon.Open();

                using (MySqlDataReader lector = comando.ExecuteReader())
                {
                    tabla.Load(lector);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar usuarios: " + ex.Message);
            }
            finally
            {
                if (sqlCon != null && sqlCon.State == ConnectionState.Open)
                    sqlCon.Close();
            }

            return tabla;
        }


        //Método para modificar datos del usuario
        public string ModificarUsuario(Usuario user)
        {
            MySqlConnection sqlCon = null;
            string respuesta = "";

            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion();
                MySqlCommand comando = new MySqlCommand("ModificarUsuario", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("p_id_usuario", MySqlDbType.Int32).Value = user.IdUsuario;
                comando.Parameters.Add("p_nombre_usuario", MySqlDbType.VarChar).Value = user.NombreUsuario;
                comando.Parameters.Add("p_contrasena_hash", MySqlDbType.VarChar).Value = user.ContrasenaHash;
                comando.Parameters.Add("p_rol", MySqlDbType.VarChar).Value = user.Rol;

                sqlCon.Open();
                int filasAfectadas = comando.ExecuteNonQuery();

                respuesta = (filasAfectadas >= 1) ? "OK" : "No se pudo modificar el usuario.";
            }
            catch (Exception ex)
            {
                respuesta = "Error de SQL: " + ex.Message;
            }
            finally
            {
                if (sqlCon != null && sqlCon.State == ConnectionState.Open)
                    sqlCon.Close();
            }

            return respuesta;
        }

        //Método para eliminar un usuario
        public string EliminarUsuario(int idUsuario)
        {
            MySqlConnection sqlCon = null;
            string respuesta = "";

            try
            {
                sqlCon = Conexion.getInstancia().CrearConexion();
                MySqlCommand comando = new MySqlCommand("EliminarUsuario", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;

                // Parámetro que recibe el procedimiento almacenado
                comando.Parameters.Add("p_idUsuario", MySqlDbType.Int32).Value = idUsuario;

                sqlCon.Open();

                int filasAfectadas = comando.ExecuteNonQuery();

                if (filasAfectadas >= 1)
                {
                    respuesta = "OK";
                }
                else
                {
                    respuesta = "No se encontró el usuario o no se eliminó ningún registro.";
                }
            }
            catch (Exception ex)
            {
                respuesta = "Error de SQL al eliminar: " + ex.Message;
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
