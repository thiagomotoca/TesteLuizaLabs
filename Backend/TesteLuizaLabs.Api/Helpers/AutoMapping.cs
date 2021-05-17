using AutoMapper;
using TesteLuizaLabs.Api.Models;
using TesteLuizaLabs.Aplicacao.Entidades;

namespace TesteLuizaLabs.Api.Helpers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<UsuarioPostDto, Usuario>();
            CreateMap<Usuario, UsuarioGetDto>();
            CreateMap<UsuarioPutDto, Usuario>();
        }
    }
}
