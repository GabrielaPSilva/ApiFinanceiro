using APIFinanceiro.Business.Services;
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

        [HttpGet("CPF/{CPF}")]
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

        [HttpPut("aplicacao/idUsuario/{idUsuario}/idSegmento/{idSegmento}")]
        public async Task<IActionResult> InserirAtualizarAplicacao(int idUsuario, int idSegmento, [FromBody] AplicacaoModel aplicacao)
        {
            try
            {
                if (!aplicacao.IsValid(out string mensagemErro))
                {
                    return BadRequest(new { erro = mensagemErro });
                }

                var retorno = await _investimentoService.Aplicar(aplicacao, idUsuario, idSegmento);

                if (retorno != null)
                {
                    return Ok(retorno);
                }

                return BadRequest(new { erro = "Erro realizar aplicacação" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("resgate/idUsuario/{idUsuario}/idSegmento/{idSegmento}")]
        public async Task<IActionResult> InserirAtualizarResgate(int idUsuario, int idSegmento, [FromBody] ResgateModel resgate)
        {
            try
            {
                if (!resgate.IsValid(out string mensagemErro))
                {
                    return BadRequest(new { erro = mensagemErro });
                }

                var retorno = await _investimentoService.Resgatar(resgate, idUsuario, idSegmento);

                if (retorno != null)
                {
                    return Ok(retorno);
                }

                return BadRequest(new { erro = "Erro realizar resgate" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
