using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultura_BCN
{
    public class ReservasEntradas
    {
        [Key]
        public int id_reserva {  get; set; }
        public int id_asiento { get; set; }

        public int id_usuario { get; set; }

        public DateTime fecha_reserva { get; set; }
        public ReservasEntradas(int id_reserva, int id_asiento, int id_usuario, DateTime fecha_reserva)
        {
            this.id_reserva = id_reserva;
            this.id_asiento = id_asiento;
            this.id_usuario = id_usuario;
            this.fecha_reserva = fecha_reserva;
        }
        public ReservasEntradas() { }

    }
}
