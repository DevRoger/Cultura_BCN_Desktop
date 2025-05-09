using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultura_BCN
{
    public class Chats
    {
        [Key]
        public int id_chat {  get; set; }
        public int id_usuario_1 { get; set; }
        public int id_usuario_2 { get; set; }
        public DateTime fecha_creacion { get; set; }

        public Chats(int id_chat, int id_usuario_1, int id_usuario_2, DateTime fecha_creacion) { 
            this.id_chat = id_chat;
            this.id_usuario_1 = id_usuario_1;
            this.id_usuario_2 = id_usuario_2;
            this.fecha_creacion = fecha_creacion;
        }
        public Chats() { }
    }
}
