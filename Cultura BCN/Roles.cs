using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultura_BCN
{
    public class Roles
    {
        [Key]
        public int id_rol {  get; set; }
        public string nombre { get; set; }
        public Roles(int id_rol, string nombre)
        {
            this.id_rol = id_rol;
            this.nombre = nombre;
        }
    }
}
