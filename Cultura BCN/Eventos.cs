using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Cultura_BCN
{
    public class Eventos
    {
        [Key]
        public int id_evento {  get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha { get; set; }
        public TimeSpan hora_inicio { get; set; }
        public TimeSpan hora_fin { get; set; }
        public string lugar { get; set; }
        public int precio { get; set; }
        public int edad_minima { get; set; }
        public bool enumerado { get; set; }
        public int id_sala { get; set; }

        public Eventos()
        {

        }
        public Eventos(int id_evento, string nombre, string descripcion, DateTime fecha, TimeSpan hora_inicio, TimeSpan hora_fin, string lugar, int precio, int edad_minima, bool enumerado, int id_sala)
        {
            this.id_evento
            = id_evento;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.fecha = fecha;
            this.hora_fin = hora_fin;
            this.hora_inicio = hora_inicio;
            this.lugar = lugar;
            this.precio = precio;
            this.edad_minima = edad_minima;
            this.enumerado = enumerado;
            this.id_sala = id_sala;
        }

    }
}
