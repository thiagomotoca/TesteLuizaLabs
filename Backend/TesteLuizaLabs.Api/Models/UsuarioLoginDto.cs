using System.ComponentModel.DataAnnotations;

namespace TesteLuizaLabs.Api.Models
{
    public class UsuarioLoginDto
    {
        [Required]
        public string Email{ get; set; }

        [Required]
        public string Senha { get; set; }
    }
}