using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultura_BCN
{
    public class Mensajes
    {
        [Key]
        public int id_mensaje { get; set; }
        public int id_chat { set; get; }
        public int id_usuario { set; get; }
        public string texto { set; get; }
        public DateTime fecha_envio { set; get; }
        public bool visto { set; get; }

        public Mensajes(int id_mensaje, int id_chat, int id_usuario, string texto, DateTime fecha_envio, bool visto)
        {
            this.texto = texto;
            this.visto = visto;
            this.fecha_envio = fecha_envio;
            this.id_mensaje = id_mensaje;
            this.id_chat = id_chat;
            this.id_usuario = id_usuario;
        }
        public Mensajes() { }
    }
}
