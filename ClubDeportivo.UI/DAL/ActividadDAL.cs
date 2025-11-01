using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using ClubDeportivo.UI.Entidades; // Asumo que tus entidades están en este namespace

namespace ClubDeportivo.UI.DAL
{
     public class ActividadDAL
     {
            public List<Actividad> ObtenerTodasActividades()
            {
                var lista = new List<Actividad>();
                // Solo necesitamos id, nombre y costo para el ComboBox.
                string consulta = "SELECT id_actividad, nombre, costo FROM actividades ORDER BY nombre";

                // Usamos tu método CrearConexion() del Singleton
                using (MySqlConnection conexion = Conexion.getInstancia().CrearConexion())
                {
                    try
                    {
                        conexion.Open();
                        using (var cmd = new MySqlCommand(consulta, conexion))
                        {
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    lista.Add(new Actividad                                    {
                                        IdActividad = reader.GetInt32("id_actividad"),
                                        Nombre = reader.GetString("nombre"),
                                        // Aseguramos que el campo 'costo' se lea como decimal
                                        Costo = reader.GetDecimal("costo")
                                    });
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // En la DAL, es mejor lanzar una excepción para que la BLL la maneje
                        throw new Exception("Error al obtener actividades de la base de datos: " + ex.Message);
                    }
                    finally
                    {
                        // Aseguramos que la conexión se cierre
                        if (conexion != null && conexion.State == ConnectionState.Open)
                        {
                            conexion.Close();
                        }
                    }
                }
                return lista;
            }


        //marian
        public List<Actividad> ObtenerTodas()
        {
            List<Actividad> lista = new List<Actividad>();
            MySqlConnection conexion = null;

            try
            {
                conexion = Conexion.getInstancia().CrearConexion();
                conexion.Open();

                string query = "SELECT id_actividad, nombre, descripcion, costo FROM actividades;";
                MySqlCommand cmd = new MySqlCommand(query, conexion);

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Actividad
                        {
                            IdActividad = Convert.ToInt32(dr["id_actividad"]),
                            Nombre = dr["nombre"].ToString(),
                            Descripcion = dr["descripcion"].ToString(),
                            Costo = Convert.ToDecimal(dr["costo"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las actividades. Detalle: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }

            return lista;
        }

        public void Insertar(Actividad act)
        {
            MySqlConnection conexion = null;

            try
            {
                conexion = Conexion.getInstancia().CrearConexion();
                conexion.Open();

                MySqlCommand cmd = new MySqlCommand("sp_InsertarActividad", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("p_nombre", act.Nombre);
                cmd.Parameters.AddWithValue("p_horario", act.Descripcion);
                cmd.Parameters.AddWithValue("p_costo", act.Costo);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la actividad: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        public void Actualizar(Actividad act)
        {
            MySqlConnection conexion = null;

            try
            {
                conexion = Conexion.getInstancia().CrearConexion();
                conexion.Open();

                MySqlCommand cmd = new MySqlCommand("pa_actualizar_actividad", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("p_id", act.IdActividad);
                cmd.Parameters.AddWithValue("p_nombre", act.Nombre);
                cmd.Parameters.AddWithValue("p_horario", act.Descripcion);
                cmd.Parameters.AddWithValue("p_costo", act.Costo);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la actividad: " + ex.Message);
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }

        }
        public void Eliminar(int idActividad)
        {
            using (MySqlConnection conexion = Conexion.getInstancia().CrearConexion())
            {
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM actividades WHERE id_actividad = @id", conexion);
                cmd.Parameters.AddWithValue("@id", idActividad);
                cmd.ExecuteNonQuery();
            }
        }

    }


}


    

