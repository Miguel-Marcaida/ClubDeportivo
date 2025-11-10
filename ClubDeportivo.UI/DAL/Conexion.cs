using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using ClubDeportivo.UI.Utilitarios;

namespace ClubDeportivo.UI.DAL
{
    // DTO: Objeto de configuración interna que reemplaza las variables hardcodeadas
    internal class ConfiguracionConexionDTO
    {
        public string Servidor { get; set; } = "localhost"; // Valores por defecto
        public string BaseDatos { get; set; } = "comC_grupo14";
        public string Usuario { get; set; } = "root";
        public string Contrasena { get; set; } = string.Empty;
        public string Puerto { get; set; } = "3306";
    }

    // Usaremos el patrón Singleton para asegurar que solo haya una instancia de conexión.
    public sealed class Conexion
    {
        private static Conexion? instacia = null;
        private static ConfiguracionConexionDTO _config = new ConfiguracionConexionDTO();
        private const string RUTA_CONFIG = "ConexionConfig.ini"; // Nombre del archivo de persistencia

        // Constructor privado: se ejecuta SOLO la primera vez (por el Singleton)
        private Conexion()
        {
            // La única acción del constructor es cargar o configurar la conexión
            CargarOConfigurarConexion();
        }

        // =========================================================================
        // LÓGICA DE PERSISTENCIA (Leer y Escribir el Archivo .INI)
        // =========================================================================

        private void EscribirConfiguracion()
        {
            try
            {
                string[] contenido = new string[]
                {
                    $"Servidor={_config.Servidor}",
                    $"BaseDatos={_config.BaseDatos}",
                    $"Usuario={_config.Usuario}",
                    $"Contrasena={_config.Contrasena}",
                    $"Puerto={_config.Puerto}"
                };
                File.WriteAllLines(RUTA_CONFIG, contenido);
            }
            catch (Exception ex)
            {
                Prompt.MostrarAlerta("Advertencia: No se pudo guardar la configuración de conexión. Verifique permisos. Error: " + ex.Message,
                                     "Error de Persistencia");
            }
        }


        private void LeerConfiguracion()
        {
            try
            {
                var lineas = File.ReadAllLines(RUTA_CONFIG);
                var configDict = lineas
                    .Where(line => !string.IsNullOrWhiteSpace(line) && line.Contains("="))
                    .Select(line => line.Split(new[] { '=' }, 2))
                    .ToDictionary(parts => parts[0].Trim(), parts => parts[1].Trim(), StringComparer.OrdinalIgnoreCase);

                // 🚨 CORRECCIÓN: Usamos variables locales (temp) y luego asignamos a _config.
                string tempValue = string.Empty;

                if (configDict.TryGetValue("Servidor", out tempValue))
                    _config.Servidor = tempValue;

                if (configDict.TryGetValue("BaseDatos", out tempValue))
                    _config.BaseDatos = tempValue;

                if (configDict.TryGetValue("Usuario", out tempValue))
                    _config.Usuario = tempValue;

                if (configDict.TryGetValue("Contrasena", out tempValue))
                    _config.Contrasena = tempValue;

                if (configDict.TryGetValue("Puerto", out tempValue))
                    _config.Puerto = tempValue;
            }
            catch (Exception)
            {
                // En caso de error de lectura, se vuelve a configurar.
                
            }
        }



        // =========================================================================
        // LÓGICA DE AUTOCONFIGURACIÓN (El método de decisión)
        // =========================================================================


        private void CargarOConfigurarConexion()
        {
            // Creamos una instancia temporal solo para obtener los valores por defecto del DTO.
            // Si el DTO no tiene constructor, debemos asegurarnos que inicializa las propiedades.
            ConfiguracionConexionDTO defaultConfig = new ConfiguracionConexionDTO();

            // 1. Intentar leer la configuración guardada
            if (File.Exists(RUTA_CONFIG))
            {
                LeerConfiguracion();
                return;
            }

            // 2. Si no existe, usamos el Prompt personalizado (Primera vez)
            try
            {
                bool correcto = false;
                while (!correcto)
                {
                    // 🚨 USAMOS LA SOBRECARGA con los VALORES POR DEFECTO del DTO
                    string serv = Prompt.ShowDialog("Ingrese el Servidor/IP de MySQL:", "CONFIGURACIÓN INICIAL (1 de 5)", defaultConfig.Servidor);
                    string port = Prompt.ShowDialog("Ingrese el Puerto de MySQL (usualmente 3306):", "CONFIGURACIÓN INICIAL (2 de 5)", defaultConfig.Puerto);
                    string user = Prompt.ShowDialog("Ingrese el Usuario de MySQL (ej: root):", "CONFIGURACIÓN INICIAL (3 de 5)", defaultConfig.Usuario);
                    string pass = Prompt.ShowDialog("Ingrese la Contraseña de MySQL:", "CONFIGURACIÓN INICIAL (4 de 5)", string.Empty); // Contraseña siempre vacía
                    string db = Prompt.ShowDialog("Ingrese el Nombre de la Base de Datos:", "CONFIGURACIÓN INICIAL (5 de 5)", defaultConfig.BaseDatos);

                    // Verificación básica de datos 
                    if (string.IsNullOrEmpty(serv) || string.IsNullOrEmpty(db) || string.IsNullOrEmpty(user))
                    {
                        Prompt.MostrarAlerta("Debe ingresar los datos de Servidor, Base de Datos y Usuario para continuar.");
                        continue;
                    }

                    // Pide confirmación usando su Prompt.MostrarDialogoSiNo
                    DialogResult result = Prompt.MostrarDialogoSiNo(
                        $"Confirma los datos ingresados?\n\nServidor: {serv}\nPuerto: {port}\nUsuario: {user}\nBase de Datos: {db}",
                        "CONFIRMAR CONEXIÓN");

                    if (result == DialogResult.Yes)
                    {
                        _config.Servidor = serv;
                        _config.Puerto = port;
                        _config.Usuario = user;
                        _config.Contrasena = pass;
                        _config.BaseDatos = db;
                        correcto = true;
                        EscribirConfiguracion();
                    }
                }
            }
            catch (Exception ex)
            {
                Prompt.MostrarError($"Error fatal en la configuración inicial. La aplicación debe cerrarse. {ex.Message}", "ERROR CRÍTICO");
                throw new Exception("Error fatal en la configuración inicial: " + ex.Message);
            }
        }


        // =========================================================================
        // SINGLETON Y CREACIÓN DE CONEXIÓN
        // =========================================================================

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
            if (_config == null || string.IsNullOrEmpty(_config.Servidor))
            {
                // Esto solo ocurriría si el archivo .INI es borrado o está corrupto
                throw new InvalidOperationException("La configuración de conexión no fue cargada o es inválida. Inicie la aplicación nuevamente.");
            }

            MySqlConnection cadena = new MySqlConnection();
            try
            {
                // Usamos los valores cargados/configurados
                cadena.ConnectionString = $"Server={_config.Servidor};" +
                                          $"Port={_config.Puerto};" +
                                          $"Database={_config.BaseDatos};" +
                                          $"Uid={_config.Usuario};" +
                                          $"Pwd={_config.Contrasena};";
            }
            catch (Exception ex)
            {
                cadena = null;
                throw new Exception("Error al crear la cadena de conexión: " + ex.Message);
            }
            return cadena;
        }
    }
}




