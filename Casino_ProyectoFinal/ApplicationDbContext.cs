using Casino_ProyectoFinal.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Casino_ProyectoFinal
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Participantes> Participantes { get; set; }

    }
}
