using APIFinanceiro.Business.Services.Interfaces;
using APIFinanceiro.Data.Repositories;
using APIFinanceiro.Data.Repositories.Interfaces;
using APIFinanceiro.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFinanceiro.Business.Services
{
    public class RiscoService : IRiscoService
    {
        private readonly IRiscoRepository _riscoRepository;

        public RiscoService(IRiscoRepository riscoRepository)
        {
            _riscoRepository = riscoRepository;
        }

        public async Task<List<RiscoModel>> ListarRisco()
        {
            return await _riscoRepository.ListarRisco();
        }

        public async Task<RiscoModel> RetornarRiscoDescricao(string descricao)
        {
            return await _riscoRepository.RetornarRiscoDescricao(descricao);
        }

        public async Task<int> CadastrarRisco(RiscoModel risco)
        {
            {
                if (risco == null)
                    return 0;

                return await _riscoRepository.CadastrarRisco(risco);
            }
        }

        public async Task<bool> AlterarRisco(RiscoModel risco)
        {
            if (risco == null)
                return false;

            return await _riscoRepository.AlterarRisco(risco);
        }

        public async Task<bool> RemoverRisco(int idRisco)
        {
            return await _riscoRepository.RemoverRisco(idRisco);
        }

    }
}
