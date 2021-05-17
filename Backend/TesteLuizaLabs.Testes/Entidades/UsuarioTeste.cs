using Microsoft.VisualStudio.TestTools.UnitTesting;
using TesteLuizaLabs.Aplicacao.Entidades;

namespace TesteLuizaLabs.Testes.Entidades
{
    [TestClass]
    public class UsuarioTeste
    {
        internal static Usuario RetornaObjetoValido(string email = "teste@teste.com", string nome = "Teste", string senha = "123456")
            => new Usuario
            {
                Email = email,
                Id = 1,
                Nome = nome,
                Senha = senha
            };
        
        [TestMethod]
        public void Usuario_Valido()
        {
            var usuario = RetornaObjetoValido();
            var valido = usuario.Valido();
            Assert.AreEqual(true, valido);
        }

        [TestMethod]
        public void Usuario_Email_Invalido()
        {
            var usuario = RetornaObjetoValido(email: "aa12");
            var valido = usuario.Valido();
            Assert.AreEqual(false, valido);
        }

        [TestMethod]
        public void Usuario_Email_Nulo()
        {
            var usuario = RetornaObjetoValido(email: null);
            var valido = usuario.Valido();
            Assert.AreEqual(false, valido);
        }

        [TestMethod]
        public void Usuario_Nome_Nulo()
        {
            var usuario = RetornaObjetoValido(nome: null);
            var valido = usuario.Valido();
            Assert.AreEqual(false, valido);            
        }

        [TestMethod]
        public void Usuario_Nome_Maior120()
        {
            var usuario = RetornaObjetoValido(nome: new string('a', 121));
            var valido = usuario.Valido();
            Assert.AreEqual(false, valido);            
        }

        [TestMethod]
        public void Usuario_Senha_Nula()
        {
            var usuario = RetornaObjetoValido(senha: null);
            var valido = usuario.Valido();
            Assert.AreEqual(false, valido);
        }

        [TestMethod]
        public void Usuario_Senha_Maior20()
        {
            var usuario = RetornaObjetoValido(senha: new string('a', 21));
            var valido = usuario.Valido();
            Assert.AreEqual(false, valido);
        }
    }
}