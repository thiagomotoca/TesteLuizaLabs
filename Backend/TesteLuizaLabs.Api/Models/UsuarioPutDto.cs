using System.ComponentModel.DataAnnotations;

namespace TesteLuizaLabs.Api.Models
{
    public class UsuarioPutDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Senha { get; set; }

        [Required]
        public string ConfirmacaoSenha { get; set; }
    }
}