using APIFinanceiro.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFinanceiro.Data.Repositories.Interfaces
{
    public interface ISegmentoRepository
    {
        Task<List<SegmentoModel>> ListarSegmento();
        Task<List<SegmentoModel>> RetornarSegmentoTipoSegmento(string tipoSegmento);
        Task<SegmentoModel> RetornarSegmentoTipoSegmentoIdRisco(string tipoSegmento, int idRisco);
        Task<int> CadastrarSegmento(SegmentoModel segmento);
        Task<bool> AlterarSegmento(SegmentoModel segmento);
        Task<bool> RemoverSegmento(int idSegmento);

    }
}
