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

        [HttpGet("tipoSegmento/{tipoSegmento}")]
        public async Task<IActionResult> RetornarSegmentoTipoSegmento(string tipoSegmento)
        {
            try
            {
                List<SegmentoModel> listaSegmento = await _segmentoService.RetornarSegmentoTipoSegmento(tipoSegmento);

                if (listaSegmento == null || listaSegmento.Count() == 0)
                {
                    return NotFound(new { erro = "Segmento não encontrado" });
                }

                return Ok(listaSegmento);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("tipoSegmento/{tipoSegmento}/idRisco/{idRisco}")]
        public async Task<IActionResult> RetornarSegmentoTipoSegmento(string tipoSegmento, int idRisco)
        {
            try
            {
                SegmentoModel listaSegmento = await _segmentoService.RetornarSegmentoTipoSegmentoIdRisco(tipoSegmento, idRisco);

                if (listaSegmento == null)
                {
                    return NotFound(new { erro = "Segmento não encontrado" });
                }

                return Ok(listaSegmento);
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

        [HttpPut("idSegmento/{idSegmento}")]
        public async Task<IActionResult> AlterarSegmento(int idSegmento, [FromBody] SegmentoModel segmento)
        {
            try
            {
                if (!segmento.IsValid(out string mensagemErro))
                {
                    return BadRequest(new { erro = mensagemErro });
                }

                var retorno = await _segmentoService.RetornaSegmentoIdSegmento(idSegmento);

                if (retorno == null)
                {
                    return NotFound(new { erro = "Segmento não encontrado" });
                }

                var segmentoModel = new SegmentoModel()
                {
                    Id = retorno.Id,
                    IdRisco = segmento.IdRisco,
                    PercentualRendimento = segmento.PercentualRendimento,
                    MesesVigencia = segmento.MesesVigencia,
                    TipoSegmento = segmento.TipoSegmento,
                    TaxaAdm = segmento.TaxaAdm
                };

                if (await _segmentoService.AlterarSegmento(segmentoModel))
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

        [HttpDelete("tipoSegmento/{tipoSegmento}")]
        public async Task<IActionResult> Deletar(string tipoSegmento, int idRisco)
        {
            try
            {
                var retornarSegmento = await _segmentoService.RetornarSegmentoTipoSegmentoIdRisco(tipoSegmento, idRisco);

                if (retornarSegmento == null)
                {
                    return NotFound(new { erro = "Segmento não encontrado" });
                }

                var idSegmento = retornarSegmento.Id;

                if (await _segmentoService.RemoverSegmento(idSegmento))
                {
                    return NoContent();
                }

                return BadRequest(new { erro = "Erro ao deletar segmento" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
