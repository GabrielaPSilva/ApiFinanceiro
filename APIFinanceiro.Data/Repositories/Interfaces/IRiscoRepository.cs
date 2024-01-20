using APIFinanceiro.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFinanceiro.Data.Repositories.Interfaces
{
    public interface IRiscoRepository
    {
        Task<List<RiscoModel>> ListarRisco();
        Task<RiscoModel> RetornarRiscoDescricao(string Descricao);
        Task<int> CadastrarRisco(RiscoModel risco);
        Task<bool> AlterarRisco(RiscoModel risco);
        Task<bool> RemoverRisco(int idRisco);

    }
}
