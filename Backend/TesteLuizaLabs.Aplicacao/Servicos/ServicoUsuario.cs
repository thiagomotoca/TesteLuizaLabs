using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TesteLuizaLabs.Aplicacao.Entidades;
using TesteLuizaLabs.Aplicacao.Interfaces.Repositorio;
using TesteLuizaLabs.Aplicacao.Interfaces.Servico;
using TesteLuizaLabs.Aplicacao.Models;
using TesteLuizaLabs.Lib.Excecao;
using TesteLuizaLabs.Lib.Models;
using TesteLuizaLabs.Lib.Notificacao;

namespace TesteLuizaLabs.Aplicacao.Servicos
{
    public class ServicoUsuario : IServicoUsuario
    {
        private readonly AppSettings _settings;
        private readonly IRepositorioUsuario _repoUsuario;

        public ServicoUsuario(
            IOptions<AppSettings> settings,
            IRepositorioUsuario repoUsuario)
        {
            _repoUsuario = repoUsuario;
            _settings = settings.Value;
        }

        public async Task<Usuario> Adicionar(Usuario usuario)
        {
            if (!usuario.Valido())
                throw new DadoInvalidoException("Dados de usuário inválidos");

            var retorno = await _repoUsuario.PesquisarPor(x => x.Email == usuario.Email);
            if (retorno.Any())
                throw new DadoDuplicadoException("Já existe um usuário com esse email");

            usuario.Senha = CriptografaSenha(usuario.Senha);

            usuario = await _repoUsuario.Adicionar(usuario);

            var variaveis = new Dictionary<string, string>();
            variaveis.Add("nome", usuario.Nome);

            Email.DispararCadastro(
                new DisparoEmail
                {
                    De = _settings.EmailDe,
                    Para = usuario.Email,
                    Porta = _settings.EmailPorta,
                    Senha = _settings.EmailSenha,
                    Servidor = _settings.EmailServidor,
                    Usuario = _settings.EmailUsuario,
                    Variaveis = variaveis
                });

            return usuario;
        }

        public async Task<TokenUsuarioDto> Autenticar(string email, string senha)
        {
            senha = CriptografaSenha(senha);
            var usuario = (await _repoUsuario.PesquisarPor(u => u.Email == email && u.Senha == senha)).FirstOrDefault();

            if (usuario == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.Secret);
            var dataExpiracao = DateTime.UtcNow.AddHours(4);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Email.ToString())
                }),
                Expires = dataExpiracao,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new TokenUsuarioDto
            {
                Id = usuario.Id,
                NomeUsuario = usuario.Nome,
                Token = tokenHandler.WriteToken(token),
                DataExpiracao = dataExpiracao
            };
        }

        public async Task RecuperarSenha(string email)
        {
            var existeUsuario = await _repoUsuario.PesquisarPor(x => x.Email == email);

            if (!existeUsuario.Any())
                throw new DadoNaoEncontratoException("Usuário não encontrado");

            var usuario = existeUsuario.FirstOrDefault();

            var variaveis = new Dictionary<string, string>();
            variaveis.Add("nome", usuario.Nome);
            variaveis.Add("link", $"{_settings.UrlFront}/usuario/alterar-senha/{usuario.Id}");

            Email.DispararRecuperacaoSenha(
                new DisparoEmail
                {
                    De = _settings.EmailDe,
                    Para = email,
                    Porta = _settings.EmailPorta,
                    Senha = _settings.EmailSenha,
                    Servidor = _settings.EmailServidor,
                    Usuario = _settings.EmailUsuario,
                    Variaveis = variaveis
                });
        }

        public async Task AlterarSenha(Usuario usuario)
        {
            var existeUsuario = await _repoUsuario.PesquisarPor(u => u.Id == usuario.Id);

            if (!existeUsuario.Any())
                throw new DadoNaoEncontratoException("Usuário não encontrado");

            var usuarioAlterar = existeUsuario.FirstOrDefault();
            usuarioAlterar.Senha = CriptografaSenha(usuario.Senha);

            await _repoUsuario.Alterar(usuarioAlterar);

            var variaveis = new Dictionary<string, string>();
            variaveis.Add("nome", usuarioAlterar.Nome);

            Email.DispararSenhaAlterada(
                new DisparoEmail
                {
                    De = _settings.EmailDe,
                    Para = usuarioAlterar.Email,
                    Porta = _settings.EmailPorta,
                    Senha = _settings.EmailSenha,
                    Servidor = _settings.EmailServidor,
                    Usuario = _settings.EmailUsuario,
                    Variaveis = variaveis
                });
        }

        private string CriptografaSenha(string senha)
        {
            var encoding = new UnicodeEncoding();
            byte[] hashBytes;
            using (HashAlgorithm hash = SHA1.Create())
                hashBytes = hash.ComputeHash(encoding.GetBytes(senha));

            var hashValue = new StringBuilder(hashBytes.Length * 2);
            foreach (byte b in hashBytes)
            {
                hashValue.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}", b);
            }

            return hashValue.ToString();
        }
    }
}
