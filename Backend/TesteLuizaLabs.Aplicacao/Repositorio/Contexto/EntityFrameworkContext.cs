using Microsoft.EntityFrameworkCore;
using TesteLuizaLabs.Aplicacao.Entidades;
using TesteLuizaLabs.Aplicacao.Repositorio.Mapeamento;

namespace TesteLuizaLabs.Aplicacao.Repositorio.Contexto
{
    public class EntityFrameworkContext : DbContext
    {
        public EntityFrameworkContext(DbContextOptions<EntityFrameworkContext> options) : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MapeamentoUsuario());
        }
    }
}