﻿using APIFinanceiro.Business.Services;
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
                    return NotFound(new { erro = "Lista de riscos não encontrada" });
                }

                return Ok(listaRisco);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("descricao/{descricao}")]
        public async Task<IActionResult> RetornarRiscoDescricao(string descricao)
        {
            try
            {
                RiscoModel usuario = await _riscoService.RetornarRiscoDescricao(descricao);

                if (usuario == null)
                {
                    return NotFound(new { erro = "Risco não encontrado" });
                }

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarRisco([FromBody] RiscoModel risco)
        {
            try
            {
                if (!risco.IsValid(out string mensagemErro))
                {
                    return BadRequest(new { erro = mensagemErro });
                }

                var retornoCadastro = await _riscoService.CadastrarRisco(risco);

                if (retornoCadastro > 0)
                {
                    return Created($"/api/risco/{risco.Id}", retornoCadastro);
                }

                return BadRequest(new { erro = "Erro ao cadastrar risco" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("descricao/{descricao}")]
        public async Task<IActionResult> AlterarRisco(string descricao, [FromBody] RiscoModel risco)
        {
            try
            {
                if (!risco.IsValid(out string mensagemErro))
                {
                    return BadRequest(new { erro = mensagemErro });
                }

                var retornaRiscoDescricao = await _riscoService.RetornarRiscoDescricao(descricao);

                if (retornaRiscoDescricao == null)
                {
                    return NotFound(new { erro = "Risco não encontrado" });
                }

                var riscoModel = new RiscoModel()
                {
                    Id = retornaRiscoDescricao.Id,
                    GrauRisco = risco.GrauRisco,
                    Descricao = risco.Descricao
                };

                if (await _riscoService.AlterarRisco(riscoModel))
                {
                    return Ok(risco);
                }

                return BadRequest(new { erro = "Erro ao alterar o risco" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
