using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultura_BCN
{
    internal class ReservasEntradas
    {
        private int id_reserva {  get; set; }
        private int id_asiento { get; set; }

        private int id_usuario { get; set; }

        private DateTime fecha_reserva { get; set; }
        public ReservasEntradas(int id_reserva, int id_asiento, int id_usuario, DateTime fecha_reserva)
        {
            this.id_reserva = id_reserva;
            this.id_asiento = id_asiento;
            this.id_usuario = id_usuario;
            this.fecha_reserva = fecha_reserva;
        }



    }
}
