using APIFinanceiro.Business.Services.Interfaces;
using APIFinanceiro.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace APIFinanceiro.Controller
{
    [Route("api/segmento")]
    [ApiController]
    public class SegmentoController : ControllerBase
    {
        private readonly ISegmentoService _segmentoService;

        public SegmentoController(ISegmentoService segmentoService)
        {
            _segmentoService = segmentoService;
        }

        [HttpGet]
        public async Task<IActionResult> ListarSegmento()
        {
            try
            {
                List<SegmentoModel> listaSegmento = await _segmentoService.ListarSegmento();

                if (listaSegmento == null || listaSegmento.Count() == 0)
                {
                    return NotFound(new { erro = "Lista de segmentos não encontrada" });
                }

                return Ok(listaSegmento);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{tipoSegmento}")]
        public async Task<IActionResult> RetornarSegmentoTipoSegmento(string tipoSegmento)
        {
            try
            {
                SegmentoModel segmento = await _segmentoService.RetornarSegmentoTipoSegmento(tipoSegmento);

                if (segmento == null)
                {
                    return NotFound(new { erro = "Segmento não encontrado" });
                }

                return Ok(segmento);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarSegmento([FromBody] SegmentoModel segmento)
        {
            try
            {
                if (!segmento.IsValid(out string mensagemErro))
                {
                    return BadRequest(new { erro = mensagemErro });
                }

                var retornoCadastro = await _segmentoService.CadastrarSegmento(segmento);

                if (retornoCadastro > 0)
                {
                    return Created($"/api/segmento/{segmento.Id}", retornoCadastro);
                }

                return BadRequest(new { erro = "Erro ao cadastrar o segmento" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{tipoSegmento}")]
        public async Task<IActionResult> AlterarSegmento(string tipoSegmento, [FromBody] SegmentoModel segmento)
        {
            try
            {
                if (!segmento.IsValid(out string mensagemErro))
                {
                    return BadRequest(new { erro = mensagemErro });
                }

                if (await _segmentoService.RetornarSegmentoTipoSegmento(tipoSegmento) == null)
                {
                    return NotFound(new { erro = "Usuário não encontrado" });
                }

                segmento.TipoSegmento = tipoSegmento;

                if (await _segmentoService.AlterarSegmento(segmento))
                {
                    return Ok(segmento);
                }

                return BadRequest(new { erro = "Erro ao alterar o segmento" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{tipoSegmento}")]
        public async Task<IActionResult> Deletar(string tipoSegmento)
        {
            try
            {
                var retornarSegmento = await _segmentoService.RetornarSegmentoTipoSegmento(tipoSegmento);

                if (retornarSegmento == null)
                {
                    return NotFound(new { erro = "Segmento não encontrado" });
                }

                var idSegmento = retornarSegmento.Id;

                if (await _segmentoService.RemoverSegmento(idSegmento))
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
