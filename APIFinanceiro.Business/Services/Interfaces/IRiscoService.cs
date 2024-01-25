using APIFinanceiro.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFinanceiro.Business.Services.Interfaces
{
    public interface IRiscoService
    {
        Task<List<RiscoModel>> ListarRisco();
        Task<RiscoModel> RetornarRiscoDescricao(string descricao);
        Task<int> CadastrarRisco(RiscoModel risco);
        Task<bool> AlterarRisco(RiscoModel risco);
        Task<bool> RemoverRisco(int idRisco);
    }
}
