using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultura_BCN
{
    public class DTOReservations
    {   
        public int id_reserva {  get; set; }
        public string usuario {  get; set; }
        public int numero { get; set; }
        public DateTime fecha_reserva { get; set; }

        public DTOReservations(int id_reserva, string usuario, int numero, DateTime fecha_reserva) { 
            this.fecha_reserva = fecha_reserva;
            this.id_reserva = id_reserva;
            this.usuario = usuario;
            this.numero = numero;
        }
    }
}
