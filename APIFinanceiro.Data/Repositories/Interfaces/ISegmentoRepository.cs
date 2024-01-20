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
        Task<SegmentoModel> RetornarSegmentoTipoSegmento(string TipoSegmento);
        Task<int> CadastrarSegmento(SegmentoModel segmento);
        Task<bool> AlterarSegmento(SegmentoModel segmento);
        Task<bool> RemoverSegmento(int idSegmento);

    }
}
