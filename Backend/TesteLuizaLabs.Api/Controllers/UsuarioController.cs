using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TesteLuizaLabs.Api.Models;
using TesteLuizaLabs.Aplicacao.Entidades;
using TesteLuizaLabs.Aplicacao.Interfaces.Servico;
using TesteLuizaLabs.Aplicacao.Models;

namespace TesteLuizaLabs.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IServicoUsuario _servicoUsuario;
        private readonly IMapper _mapper;

        public UsuarioController(
            IServicoUsuario servicoUsuario,
            IMapper mapper
        )
        {
            _servicoUsuario = servicoUsuario;
            _mapper = mapper;
        }

        /// <summary>
        /// Cadastra um usuário novo
        /// </summary>
        /// <response code="201">Usuário cadastrado com sucesso</response>
        /// <response code="400">Dados do usuário inválidos</response>
        /// <response code="409">Já existe um usuário com esse email</response>
        /// <response code="500">Erro interno da aplicação</response>
        /// <param name="usuarioPost">Dados do usuário para cadastrar</param>
        /// <returns><see cref="UsuarioGetDto"/>Dados do usuário cadastrado</returns>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<UsuarioGetDto>> PostCadastro([FromBody] UsuarioPostDto usuarioPost)
        {
            if (usuarioPost.Senha != usuarioPost.ConfirmacaoSenha)
                return BadRequest(new { message = "Confirmação da senha é diferente da senha" });

            var usuario = _mapper.Map<Usuario>(usuarioPost);
            var novoUsuario = await _servicoUsuario.Adicionar(usuario);
            return Created("", _mapper.Map<UsuarioGetDto>(novoUsuario));
        }

        /// <summary>
        /// Faz o login do usuario
        /// </summary>
        /// <response code="200">Usuário logado com sucesso</response>
        /// <response code="400">Credenciais inválidas</response>
        /// <response code="404">Usuário não encontrado</response>
        /// <response code="500">Erro interno da aplicação</response>
        /// <param name="usuarioLogin"><see cref="UsuarioLoginDto"/>Dados de login</param>
        /// <returns><see cref="TokenUsuarioDto"/>Token de autenticação</returns>
        [AllowAnonymous]
        [HttpPost("autenticar")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<TokenUsuarioDto>> Autenticar([FromBody] UsuarioLoginDto usuarioLogin)
        {
            var token = await _servicoUsuario.Autenticar(usuarioLogin.Email, usuarioLogin.Senha);
            if (token == null)
                return BadRequest(new { message = "Credenciais inválidas" });

            return Ok(token);
        }

        /// <summary>
        /// Recupera senha do usuário
        /// </summary>
        /// <response code="200">E-mail enviado com sucesso para usuário</response>
        /// <response code="404">Usuário não encontrado</response>
        /// <response code="500">Erro interno da aplicação</response>
        /// <param name="email">Email do usuário para recuperação</param>
        [AllowAnonymous]
        [Route("recuperarsenha")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetRecuperacaoSenha([FromQuery] string email)
        {
            await _servicoUsuario.RecuperarSenha(email);
            return Ok();
        }

        /// <summary>
        /// Altera a senha de um usuário
        /// </summary>
        /// <response code="200">Senha alterada com sucesso</response>
        /// <response code="400">Dados do usuário inválidos</response>
        /// <response code="404">Usuário não encontrado</response>
        /// <response code="500">Erro interno da aplicação</response>
        /// <param name="usuarioPut">Dados do usuário para alterar a senha</param>
        [AllowAnonymous]
        [Route("alterarsenha")]
        [HttpPut]
        [ProducesResponseType(20)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> PutSenha([FromBody] UsuarioPutDto usuarioPut)
        {
            if (usuarioPut.Senha != usuarioPut.ConfirmacaoSenha)
                return BadRequest(new { message = "Confirmação da senha é diferente da senha" });

            var usuario = _mapper.Map<Usuario>(usuarioPut);
            await _servicoUsuario.AlterarSenha(usuario);
            return Ok();
        }
    }
}