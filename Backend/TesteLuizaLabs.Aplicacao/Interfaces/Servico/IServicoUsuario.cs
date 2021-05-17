using System.Threading.Tasks;
using TesteLuizaLabs.Aplicacao.Entidades;
using TesteLuizaLabs.Aplicacao.Models;

namespace TesteLuizaLabs.Aplicacao.Interfaces.Servico
{
    public interface IServicoUsuario
    {
        Task<Usuario> Adicionar(Usuario usuario);
        Task<TokenUsuarioDto> Autenticar(string email, string senha);
        Task RecuperarSenha(string email);
        Task AlterarSenha(Usuario usuario);
    }
}