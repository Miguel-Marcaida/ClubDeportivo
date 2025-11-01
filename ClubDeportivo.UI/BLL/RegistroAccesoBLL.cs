using ClubDeportivo.UI.DAL;
using ClubDeportivo.UI.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubDeportivo.UI.BLL
{
    public class RegistroAccesoBLL
    {
        private readonly RegistroAccesoDAL oRegistroAccesoDAL = new RegistroAccesoDAL();

        public int RegistrarAccesoYpago(RegistroAcceso oRegistro)
        {
            // Lógica de Negocio 1: Validación básica
            if (oRegistro.Monto <= 0)
            {
                throw new Exception("El monto del acceso debe ser mayor a cero.");
            }
            if (string.IsNullOrEmpty(oRegistro.FormaPago))
            {
                throw new Exception("La forma de pago es obligatoria.");
            }

            // Lógica de Negocio 2: Establecer la fecha de acceso/pago si no está definida
            if (oRegistro.Fecha == DateTime.MinValue)
            {
                oRegistro.Fecha = DateTime.Now; // Usamos DateTime.Now para registrar la hora exacta de acceso
            }

            // Llamada a la DAL
            return oRegistroAccesoDAL.RegistrarAcceso(oRegistro);
        }
    }
}
