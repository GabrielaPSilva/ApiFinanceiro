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
        public IActionResult Login([FromBody] UserInfoModel user)
        {
            bool resultado = ValidarUsuario(user);
            if (resultado)
            {
                var tokenString = _authorizationService.GerarToken(user);
                return Ok(new { token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }

        private bool ValidarUsuario(UserInfoModel user)
        {
            if (user.Nome == "admin" && user.Senha == "123456")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
