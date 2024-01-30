using APIFinanceiro.Business.Services.Interfaces;
using APIFinanceiro.Data.Repositories.Interfaces;
using APIFinanceiro.Model;
using APIFinanceiro.Model.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace APIFinanceiro.Business.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly IAutenticacaoRepository _autenticationRepository;
        private readonly IConfiguration _config;

        public AutenticacaoService(IAutenticacaoRepository autenticationRepository)
        {
            _autenticationRepository = autenticationRepository;
        }

        public async Task<UsuarioPermissaoModel> ObterUsuarioNomeSenha(string nome, string senha)
        {
            return await _autenticationRepository.ObterUsuarioNomeSenha(nome, senha);
        }

        public string GerarToken(UsuarioPermissaoModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["TokenManagement:Secret"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Nome!),
                    new Claim(ClaimTypes.Role, user.Permissao.ToString()!),
                    new Claim("Id", user.Id.ToString()!)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
