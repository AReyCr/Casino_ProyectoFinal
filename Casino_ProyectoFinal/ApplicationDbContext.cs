using Casino_ProyectoFinal.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Casino_ProyectoFinal
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Participantes> Participantes { get; set; }
        public DbSet<Rifas> Rifas { get; set; }

        public DbSet<CredencialUsuario> credencialUsuarios { get; set; }

        public DbSet<RespuestaAutenticacion> respuestaAutenticaciones { get; set; }

    }
}
