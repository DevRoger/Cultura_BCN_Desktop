using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultura_BCN
{
    public class Asientos
    {
        [Key]
        public int id_asiento {  get; set; }
        public int numero { get; set; }
        public int id_evento { get; set; }
        public bool disponible { get; set; }
        public Asientos(int id_asiento, int numero, int id_evento, bool disponible)
        {
            this.id_asiento = id_asiento;
            this.numero = numero;
            this.id_evento = id_evento;
            this.disponible = disponible;
        }
    }
}
