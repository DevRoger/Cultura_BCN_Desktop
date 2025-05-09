using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultura_BCN
{
    public class Usuarios
    {
        [Key]
        public int id_usuario {  get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string correo { get; set; }
        public string contrasena_hash { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public int edad { get; set; }
        public string telefono { get; set; }
        public string foto_url { get; set; }
        public int id_rol { get; set; }
        public Usuarios(int id_usuario, string nombre, string apellidos, string correo, string contrasena_hash, DateTime fecha_nacimiento, string telefono, string foto_url, int id_rol)
        {
            this.apellidos = apellidos;
            this.id_usuario = id_usuario;
            this.nombre = nombre;
            this.telefono = telefono;
            this.correo = correo;
            this.contrasena_hash = contrasena_hash;
            this.fecha_nacimiento = fecha_nacimiento;
            this.id_rol = id_rol;
            this.foto_url = foto_url;
        }
        public Usuarios() { }
    }
}
