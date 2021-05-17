using TesteLuizaLabs.Lib.Validacao;

namespace TesteLuizaLabs.Aplicacao.Entidades
{
    public class Usuario : Entidade
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public override bool Valido()
        {
            return (!string.IsNullOrEmpty(Nome) && Nome.Length <= 120)
                && (Validacao.ValidaEmail(Email))
                && (!string.IsNullOrEmpty(Senha) && Senha.Length <= 20);
        }
    }
}