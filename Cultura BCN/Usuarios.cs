using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultura_BCN
{
    internal class Usuarios
    {
        private int id_usuario {  get; set; }
        private string nombre { get; set; }
        private string apellidos { get; set; }
        private string correo { get; set; }
        private string contrasena_hash { get; set; }
        private DateTime fecha_nacimiento { get; set; }
        private int edad { get; set; }
        private string telefono { get; set; }
        private string foto_url { get; set; }
        private int id_rol { get; set; }
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

    }
}
