using TesteLuizaLabs.Aplicacao.Entidades;
using TesteLuizaLabs.Aplicacao.Interfaces.Repositorio;
using TesteLuizaLabs.Aplicacao.Repositorio.Contexto;

namespace TesteLuizaLabs.Aplicacao.Repositorio
{
    public class RepositorioUsuario : Repositorio<Usuario>, IRepositorioUsuario
    {
        public RepositorioUsuario(EntityFrameworkContext context) : base(context) { }
    }
}