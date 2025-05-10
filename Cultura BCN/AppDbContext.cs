using System.Data.Entity;
using Cultura_BCN.Model;

namespace Cultura_BCN
{
    public class AppDbContext : DbContext
    {
       
        public AppDbContext() : base("name=CulturaBCNEntities") { }

        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Salas> Salas { get; set; }
        public DbSet<Eventos> Eventos { get; set; }
        public DbSet<ReservasEntradas> ReservasEntradas { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Mensajes> Mensajes { get; set; }
        public DbSet<Chats> Chats { get; set; }
        public DbSet<Asientos> Asientos { get; set; }
    }
}
