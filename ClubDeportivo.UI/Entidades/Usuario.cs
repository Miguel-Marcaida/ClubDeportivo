using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubDeportivo.UI.Entidades
{
    public class Usuario
    {
        // El compilador de C# crea el campo privado automáticamente por ti.
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        // La propiedad que recibirá el HASH de la DB (columna 'contrasena')
        public string ContrasenaHash { get; set; }
        public string Rol { get; set; }
    }
}
