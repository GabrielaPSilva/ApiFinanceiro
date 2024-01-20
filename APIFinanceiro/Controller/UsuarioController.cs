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

                if (listaUsuarios == null)
                {
                    return NotFound(new { erro = "Lista de clientes não encontrada" });
                }

                return Ok(listaUsuarios);
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
                //if (!tipoTelefone.IsValid(out string mensagemErro))
                //{
                //    return BadRequest(new { erro = mensagemErro });
                //}

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
    }
}
