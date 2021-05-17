using System.Collections.Generic;

namespace TesteLuizaLabs.Lib.Models
{
    public class DisparoEmail
    {
        public string Servidor { get; set; }
        public string Porta { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string De { get; set; }
        public string Para { get; set; }
        public string Assunto { get; set; }
        public string Mensagem { get; set; }
        public Dictionary<string, string> Variaveis { get; set; }
    }
}