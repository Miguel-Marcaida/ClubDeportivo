using BCrypt.Net;              // ¡Necesario para la seguridad!
using ClubDeportivo.UI.DAL;    // Para acceder a la Capa de Datos
using ClubDeportivo.UI.Entidades; // Para usar el objeto Usuario
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubDeportivo.UI.BLL
{
    public class UsuarioBLL
    {
        // Instancia privada de la Capa de Datos (DAL)
        private UsuarioDAL oUsuarioDAL = new UsuarioDAL();

        
        // El método ahora devuelve el objeto Usuario, o null
        public Usuario IniciarSesion(string usuario, string pass)
        {
            try
            {
                string user = usuario.Trim();
                string password = pass.Trim();

                // Paso 1: Llamar a DAL para OBTENER el objeto Usuario (con el hash)
                Usuario oUser = oUsuarioDAL.ValidarLogin(user);

                if (oUser == null)
                {
                    return null; // El usuario no existe. Devuelve null.
                }

                // Paso 2: Verificación del hash
                if (BCrypt.Net.BCrypt.Verify(password, oUser.ContrasenaHash))
                {
                    return oUser; // ¡RETORNA EL OBJETO COMPLETO!
                }

                return null; // Contraseña incorrecta. Devuelve null.
            }
            catch (Exception ex)
            {
                // Esto captura errores críticos de DAL/Conexión.
                // Se recomienda devolver null y que la UI muestre el mensaje de error de conexión.
                throw new Exception("Error fatal en el sistema (BLL): " + ex.Message);
                // return null; 
            }
        }

        // 2. Método para Registrar un Usuario
        public string RegistrarUsuario(string usuario, string pass, string rol)
        {
            // Lógica de Seguridad: Hashear la contraseña
            string hashPassword = BCrypt.Net.BCrypt.HashPassword(pass.Trim(), 12);

            // Crear objeto Entidad con el HASH
            Usuario oUser = new Usuario { NombreUsuario = usuario.Trim(), ContrasenaHash = hashPassword,Rol= rol.Trim() };

            // Llamar a DAL para guardar
            // Retorna "OK" si la DAL lo insertó correctamente.
            return oUsuarioDAL.InsertarUsuario(oUser);
        }

        //3. Método para listar los usarios
        public DataTable ListarUsuarios()
        {
            try
            {
                return oUsuarioDAL.ListarUsuarios();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de negocio al listar usuarios: " + ex.Message);
            }
        }


        //4. Método para modificar datos del usuario
        public string ModificarUsuario(int idUsuario, string nombre, string contrasena, string rol)
        {
            try
            {
                // Hashear nuevamente la contraseña
                string hashPassword = BCrypt.Net.BCrypt.HashPassword(contrasena.Trim(), 12);

                Usuario oUser = new Usuario
                {
                    IdUsuario = idUsuario,
                    NombreUsuario = nombre.Trim(),
                    ContrasenaHash = hashPassword,
                    Rol = rol.Trim()
                };

                return oUsuarioDAL.ModificarUsuario(oUser);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa de negocio al modificar usuario: " + ex.Message);
            }
        }

        //5. Método para eliminar un usuario
        public string EliminarUsuario(int idUsuario)
        {
            return oUsuarioDAL.EliminarUsuario(idUsuario);
        }

    }
}
