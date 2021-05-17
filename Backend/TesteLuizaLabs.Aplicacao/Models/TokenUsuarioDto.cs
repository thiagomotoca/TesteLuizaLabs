using System;

namespace TesteLuizaLabs.Aplicacao.Models
{
    public class TokenUsuarioDto
    {
        public int Id { get; set; }
        public string NomeUsuario { get; set; }
        public string Token { get; set; }
        public DateTime DataExpiracao { get; set; }
    }
}