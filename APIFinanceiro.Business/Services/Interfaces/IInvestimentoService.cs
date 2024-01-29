using APIFinanceiro.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFinanceiro.Business.Services.Interfaces
{
    public interface IInvestimentoService
    {
        Task<List<UsuarioModel>> ListaInvestimentoPorSegmentoPeloCPFUsuario(string CPF);
        Task<InvestimentoModel> RetornaInvestimentoUsuarioSegmento(int idUsuario, int idSegmento);
        Task<InvestimentoModel> Aplicar(AplicacaoModel aplicacao, int idUsuario, int idSegmento);
        Task<InvestimentoModel> Resgatar(ResgateModel resgate, int idUsuario, int idSegmento);
        Task<bool> AtualizarInvestimento(InvestimentoModel investimento);
    }
}
