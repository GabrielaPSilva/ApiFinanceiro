using APIFinanceiro.Business.Services.Interfaces;
using APIFinanceiro.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace APIFinanceiro.Controller
{
    [Route("api/investimento")]
    [ApiController]
    public class InvestimentoController : ControllerBase
    {
        private readonly IInvestimentoService _investimentoService;

        public InvestimentoController(IInvestimentoService investimentoService)
        {
            _investimentoService = investimentoService;
        }

        [HttpGet("{CPF}")]
        public async Task<IActionResult> ListarInvestimentosPorSegmentoPeloCPFUsuario(string CPF)
        {
            try
            {
                List<UsuarioModel> listaUsuarios = await _investimentoService.ListaInvestimentoPorSegmentoPeloCPFUsuario(CPF);

                if (listaUsuarios == null || listaUsuarios.Count() == 0)
                {
                    return NotFound(new { erro = $"Investimentos para o usuário do CPF {CPF} não foram encontrados" });
                }

                return Ok(listaUsuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
