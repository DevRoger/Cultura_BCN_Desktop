using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultura_BCN
{
    internal class Roles
    {
        private int id_rol {  get; set; }
        private string nombre { get; set; }
        public Roles(int id_rol, string nombre)
        {
            this.id_rol = id_rol;
            this.nombre = nombre;
        }
    }
}
