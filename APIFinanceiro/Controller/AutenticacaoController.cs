using APIFinanceiro.Business.Services.Interfaces;
using APIFinanceiro.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIFinanceiro.Controller
{
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAutenticacaoService _authorizationService;

        public AutenticacaoController(IAutenticacaoService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] UsuarioPermissaoModel user)
        {
            var usuario = _authorizationService.ObterUsuarioNomeSenha(user.Nome, user.Senha);

            if (usuario == null)
            {
                return NotFound(new { erro = "Usuário e/ou senha inválidos" });
            }

            var tokenString = _authorizationService.GerarToken(user);

            if (tokenString != null)
            {
                user.Senha = string.Empty;

                return Ok(new { usuario = user.Nome, token = tokenString });
            }

            return Unauthorized();
        }
    }
}
