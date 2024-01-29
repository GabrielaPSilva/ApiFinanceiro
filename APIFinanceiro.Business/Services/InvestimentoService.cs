using APIFinanceiro.Business.Services.Interfaces;
using APIFinanceiro.Data.Repositories.Interfaces;
using APIFinanceiro.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFinanceiro.Business.Services
{
    public class InvestimentoService : IInvestimentoService
    {
        private readonly IInvestimentoRepository _investimentoRepository;

        public InvestimentoService(IInvestimentoRepository investimentoRepository)
        {
            _investimentoRepository = investimentoRepository;
        }

        public async Task<List<UsuarioModel>> ListaInvestimentoPorSegmentoPeloCPFUsuario(string CPF)
        {
            return await _investimentoRepository.ListaInvestimentoPorSegmentoPeloCPFUsuario(CPF);
        }
    }
}
