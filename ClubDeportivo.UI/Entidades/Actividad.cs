using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubDeportivo.UI.Entidades
{
    public class Actividad
    {
        public int IdActividad { get; set; }//Id_Actividad
        public string Nombre { get; set; }
        public string Descripcion { get; set; }//Horario
        public decimal Costo { get; set; }
        //public decimal CostoMensual { get; set; }

        public Actividad() { }
    }
}
