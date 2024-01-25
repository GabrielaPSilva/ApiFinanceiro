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

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> ListarUsuario()
        {
            try
            {
                List<UsuarioModel> listaUsuarios = await _usuarioService.ListarUsuario();

                if (listaUsuarios == null || listaUsuarios.Count() == 0)
                {
                    return NotFound(new { erro = "Lista de usuários não encontrada" });
                }

                return Ok(listaUsuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{CPF}")]
        public async Task<IActionResult> Retornar(string CPF)
        {
            try
            {
                UsuarioModel usuario = await _usuarioService.RetornarUsuarioCPF(CPF);

                if (usuario == null)
                {
                    return NotFound(new { erro = "Usuário não encontrado" });
                }

                return Ok(usuario);
            }
            catch (Exception ex)
            {
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
                    return BadRequest(new { erro = mensagemErro });
                }

                var retornoCadastro = await _usuarioService.CadastrarUsuario(usuario);

                if (retornoCadastro > 0)
                {
                    return Created($"/api/usuario/{usuario.Id}", retornoCadastro);
                }

                return BadRequest(new { erro = "Erro ao cadastrar usuário" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{CPF}")]
        public async Task<IActionResult> Alterar(string CPF, [FromBody] UsuarioModel usuario)
        {
            try
            {
                if (!usuario.IsValid(out string mensagemErro))
                {
                    return BadRequest(new { erro = mensagemErro });
                }

                if (await _usuarioService.RetornarUsuarioCPF(CPF) == null)
                {
                    return NotFound(new { erro = "Usuário não encontrado" });
                }

                usuario.CPF = CPF;

                if (await _usuarioService.AlterarUsuario(usuario))
                {
                    return Ok(usuario);
                }

                return BadRequest(new { erro = "Erro ao alterar usuário" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{CPF}")]
        public async Task<IActionResult> Deletar(string CPF)
        {
            try
            {
                var retornarUsuario = await _usuarioService.RetornarUsuarioCPF(CPF);

                if (retornarUsuario == null)
                {
                    return NotFound(new { erro = "Usuário não encontrado" });
                }

                var idUsuario = retornarUsuario.Id;

                if (await _usuarioService.DesativarUsuario(idUsuario))
                {
                    return NoContent();
                }

                return BadRequest(new { erro = "Erro ao deletar usuário" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
