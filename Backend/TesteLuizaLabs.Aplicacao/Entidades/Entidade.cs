using TesteLuizaLabs.Aplicacao.Interfaces.Repositorio;

namespace TesteLuizaLabs.Aplicacao.Entidades
{
    public abstract class Entidade : IEntidade
    {
        public abstract bool Valido();
    }
}