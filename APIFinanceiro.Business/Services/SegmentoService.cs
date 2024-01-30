using APIFinanceiro.Business.Services.Interfaces;
using APIFinanceiro.Data.DatabaseConnection.Interfaces;
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
    public class SegmentoService : ISegmentoService
    {
        private readonly ISegmentoRepository _segmentoRepository;

        public SegmentoService(ISegmentoRepository segmentoRepository)
        {
            _segmentoRepository = segmentoRepository;
        }

        public async Task<List<SegmentoModel>> ListarSegmento()
        {
            return await _segmentoRepository.ListarSegmento();
        }

        public async Task<List<SegmentoModel>> RetornarSegmentoTipoSegmento(string tipoSegmento)
        {
            return await _segmentoRepository.RetornarSegmentoTipoSegmento(tipoSegmento);
        }

        public async Task<SegmentoModel> RetornaSegmentoIdSegmento(int idSegmento)
        {
            return await _segmentoRepository.RetornaSegmentoIdSegmento(idSegmento);
        }

        public async Task<SegmentoModel> RetornarSegmentoTipoSegmentoIdRisco(string tipoSegmento, int idRisco)
        {
            return await _segmentoRepository.RetornarSegmentoTipoSegmentoIdRisco(tipoSegmento, idRisco);
        }

        public async Task<int> CadastrarSegmento(SegmentoModel segmento)
        {
            {
                if (segmento == null)
                    return 0;

                return await _segmentoRepository.CadastrarSegmento(segmento);
            }
        }

        public async Task<bool> AlterarSegmento(SegmentoModel segmento)
        {
            if (segmento == null)
                return false;

            return await _segmentoRepository.AlterarSegmento(segmento);
        }

        public async Task<bool> RemoverSegmento(int idSegmento)
        {
            return await _segmentoRepository.RemoverSegmento(idSegmento);
        }
    }
}
