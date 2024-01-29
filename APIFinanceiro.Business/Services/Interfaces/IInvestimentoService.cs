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
    }
}
