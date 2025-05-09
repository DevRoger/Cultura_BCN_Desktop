using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultura_BCN
{
    public class Salas
    {
        [Key]
        public int id_sala {  get; set; }
        public int aforo { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public Salas(int id_sala, int aforo, string nombre, string direccion)
        {
            this.id_sala = id_sala;
            this.aforo = aforo;
            this.nombre = nombre;
            this.direccion = direccion;
        }
        public Salas() { }
    }
}
