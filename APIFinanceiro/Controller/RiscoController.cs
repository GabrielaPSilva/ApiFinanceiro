using APIFinanceiro.Business.Services;
using APIFinanceiro.Business.Services.Interfaces;
using APIFinanceiro.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace APIFinanceiro.Controller
{
    [Route("api/risco")]
    [ApiController]
    public class RiscoController : ControllerBase
    {
        private readonly IRiscoService _riscoService;

        public RiscoController(IRiscoService riscoService)
        {
            _riscoService = riscoService;
        }

        [HttpGet]
        public async Task<IActionResult> ListaRisco()
        {
            try
            {
                List<RiscoModel> listaRisco = await _riscoService.ListarRisco();

                if (listaRisco == null || listaRisco.Count() == 0)
                {
                    return NotFound(new { erro = "Lista de risco não encontrada" });
                }

                return Ok(listaRisco);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarUsuario([FromBody] RiscoModel risco)
        {
            try
            {
                //if (!tipoTelefone.IsValid(out string mensagemErro))
                //{
                //    return BadRequest(new { erro = mensagemErro });
                //}

                var retornoCadastro = await _riscoService.CadastrarRisco(risco);

                if (retornoCadastro > 0)
                {
                    return Created($"/api/usuario/{risco.Id}", retornoCadastro);
                }

                return BadRequest(new { erro = "Erro ao cadastrar risco" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
