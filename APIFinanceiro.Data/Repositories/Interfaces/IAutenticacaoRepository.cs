using APIFinanceiro.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFinanceiro.Data.Repositories.Interfaces
{
    public interface IAutenticacaoRepository
    {
        Task<UsuarioPermissaoModel> ObterUsuarioNomeSenha(string nome, string senha);
    }
}
