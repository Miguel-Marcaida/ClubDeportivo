using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace ClubDeportivo.UI.DAL
{
    // Usaremos el patrón Singleton para asegurar que solo haya una instancia de conexión.
    public sealed class Conexion
    {
        private readonly string servidor = "localhost"; // O la IP de tu servidor
        private readonly string baseDatos = "comC_grupo14"; // 
        private readonly string usuario = "root";
        private readonly string contrasena = ""; 

        private static Conexion? instacia = null;

        private Conexion() { } // Constructor privado para el patrón Singleton

        public static Conexion getInstancia()
        {
            if (instacia == null)
            {
                instacia = new Conexion();
            }
            return instacia;
        }



        public MySqlConnection CrearConexion()
        {
            MySqlConnection cadena = new MySqlConnection();
            try
            {
                cadena.ConnectionString = "Server=" + this.servidor +
                                          ";Database=" + this.baseDatos +
                                          ";Uid=" + this.usuario +
                                          ";Pwd=" + this.contrasena;
            }
            catch (Exception ex)
            {
                cadena = null;
                // En un sistema real, error en un log.
                throw new Exception("Error al crear la cadena de conexión: " + ex.Message);
            }
            return cadena;
        }
    }
}
