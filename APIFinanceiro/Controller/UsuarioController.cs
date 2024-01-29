using APIFinanceiro.Business.Services.Interfaces;
using APIFinanceiro.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace APIFinanceiro.Controller
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<UsuarioController> _logger;
        public UsuarioController(IUsuarioService usuarioService, ILogger<UsuarioController> logger)
        {
            _usuarioService = usuarioService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> ListarUsuario()
        {
            try
            {
                List<UsuarioModel> listaUsuarios = await _usuarioService.ListarUsuario();

                if (listaUsuarios == null || listaUsuarios.Count() == 0)
                {
                    _logger.LogInformation("Lista de usuários não encontrada");
                    return NotFound(new { erro = "Lista de usuários não encontrada" });
                }

                return Ok(listaUsuarios);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Status 500, erro de servidor");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("CPF/{CPF}")]
        public async Task<IActionResult> RetornarUsuárioCPF(string CPF)
        {
            try
            {
                UsuarioModel usuario = await _usuarioService.RetornarUsuarioCPF(CPF);

                if (usuario == null)
                {
                    _logger.LogInformation("Usuário não encontrado");
                    return NotFound(new { erro = "Usuário não encontrado" });
                }

                _logger.LogInformation("Usuário retornado.");
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Status 500, erro de servidor");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarUsuario([FromBody] UsuarioModel usuario)
        {
            try
            {
                if (!usuario.IsValid(out string mensagemErro))
                {
                    _logger.LogError(mensagemErro);
                    return BadRequest(new { erro = mensagemErro });
                }

                var retornoCadastro = await _usuarioService.CadastrarUsuario(usuario);

                if (retornoCadastro > 0)
                {
                    _logger.LogInformation("Usuário cadastrado.");
                    return Created($"/api/usuario/{usuario.Id}", retornoCadastro);
                }
                _logger.LogError("Erro ao cadastrar o usuário");
                return BadRequest(new { erro = "Erro ao cadastrar o usuário" });
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Status 500, erro de servidor");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("CPF/{CPF}")]
        public async Task<IActionResult> Alterar(string CPF, [FromBody] UsuarioModel usuario)
        {
            try
            {
                if (!usuario.IsValid(out string mensagemErro))
                {
                    _logger.LogError(mensagemErro);
                    return BadRequest(new { erro = mensagemErro });
                }

                if (await _usuarioService.RetornarUsuarioCPF(CPF) == null)
                {
                    _logger.LogInformation("Usuário não encontrado");
                    return NotFound(new { erro = "Usuário não encontrado" });
                }

                usuario.CPF = CPF;

                if (await _usuarioService.AlterarUsuario(usuario))
                {
                    _logger.LogInformation("Usuário alterado.");
                    return Ok(usuario);
                }

                _logger.LogError("Erro ao alterar usuário");
                return BadRequest(new { erro = "Erro ao alterar o usuário" });
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Status 500, erro de servidor");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("CPF/{CPF}")]
        public async Task<IActionResult> Deletar(string CPF)
        {
            try
            {
                var retornarUsuario = await _usuarioService.RetornarUsuarioCPF(CPF);

                if (retornarUsuario == null)
                {
                    _logger.LogInformation("Usuário não encontrado");
                    return NotFound(new { erro = "Usuário não encontrado" });
                }

                var idUsuario = retornarUsuario.Id;

                if (await _usuarioService.DesativarUsuario(idUsuario))
                {
                    _logger.LogInformation("Usuário desativado");
                    return NoContent();
                }
                _logger.LogError("Erro ao deletar usuário");
                return BadRequest(new { erro = "Erro ao deletar usuário" });
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Status 500, erro de servidor");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
