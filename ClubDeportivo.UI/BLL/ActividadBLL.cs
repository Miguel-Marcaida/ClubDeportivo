using System;
using System.Collections.Generic;
using ClubDeportivo.UI.DAL;
using ClubDeportivo.UI.Entidades;

namespace ClubDeportivo.UI.BLL
{
    public class ActividadBLL
    {
        private readonly ActividadDAL oActividadDAL = new ActividadDAL();

        public List<Actividad> ObtenerTodasActividades()
        {
            // Simplemente pasa la llamada a la DAL, aislando la UI de los detalles de la base de datos.
            return oActividadDAL.ObtenerTodasActividades();
        }

        //mariam
        //private ActividadDAL _actividadDAL = new ActividadDAL();

        public List<Actividad> ObtenerTodas()
        {
            return oActividadDAL.ObtenerTodas();
        }

        public void Insertar(Actividad actividad)
        {
            oActividadDAL.Insertar(actividad);
        }

        public void Actualizar(Actividad actividad)
        {
            oActividadDAL.Actualizar(actividad);
        }

        public void Eliminar(int id)
        {
            oActividadDAL.Eliminar(id);
        }



    }
}
