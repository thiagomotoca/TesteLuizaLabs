using System.ComponentModel.DataAnnotations;

namespace TesteLuizaLabs.Api.Models
{
    public class UsuarioPostDto
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }

        [Required]
        public string ConfirmacaoSenha { get; set; }
    }
}