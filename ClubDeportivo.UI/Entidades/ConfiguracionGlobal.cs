using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubDeportivo.UI.Entidades
{
    public class ConfiguracionGlobal
    {
        public int IdConfig { get; set; }
        public string Clave { get; set; }
        public string Valor { get; set; }
        public string Descripcion { get; set; }

        public ConfiguracionGlobal() { }
    }
}
