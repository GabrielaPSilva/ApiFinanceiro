using APIFinanceiro.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFinanceiro.Data.Repositories.Interfaces
{
    public interface IInvestimentoRepository
    {
        Task<List<UsuarioModel>> ListaInvestimentoPorSegmentoPeloCPFUsuario(string CPF);
        Task<InvestimentoModel> RetornaInvestimentoUsuarioSegmento(int idUsuario, int idSegmento);
        Task<int> Aplicar(AplicacaoModel aplicacao);
        Task<int> Resgatar(ResgateModel resgate);
        Task<int> CadastroInvestimento(InvestimentoModel investimento);
        Task<bool> AtualizarInvestimento(InvestimentoModel invesimento);
    }
}
