using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteLuizaLabs.Aplicacao.Entidades;

namespace TesteLuizaLabs.Aplicacao.Repositorio.Mapeamento
{
    public class MapeamentoUsuario : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuario");
            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.Id).IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Nome).HasColumnName("nome");
            builder.Property(e => e.Email).HasColumnName("email");
            builder.Property(e => e.Senha).HasColumnName("senha");
        }
    }
}
