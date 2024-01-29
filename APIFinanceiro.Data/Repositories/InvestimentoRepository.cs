using APIFinanceiro.Data.DatabaseConnection.Interfaces;
using APIFinanceiro.Data.Repositories.Interfaces;
using APIFinanceiro.Model.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFinanceiro.Data.Repositories
{
    public class InvestimentoRepository : IInvestimentoRepository
    {
        private readonly IDbSession _dbSession;

        public InvestimentoRepository(IDbSession dbSession)
        {
            _dbSession = dbSession;
        }

        public async Task<List<UsuarioModel>> ListaInvestimentoPorSegmentoPeloCPFUsuario(string CPF)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBFinanceiro");
            string query = @"
						    SELECT 
	                            Users.Id,
	                            Users.Nome,
	                            Users.CPF,
	                            Users.Email,
	                            Segm.Id,
	                            Segm.TipoSegmento,
	                            Segm.PercentualRendimento,
	                            Segm.TaxaAdm,
	                            Segm.MesesVigencia,
	                            Invest.Id,
	                            Invest.Saldo,
	                            Invest.ValorRendimento,
	                            Invest.ValorFinal
                            FROM 
	                            TB_Usuario AS Users
		                            INNER JOIN TB_Investimento AS Invest ON Users.Id = Invest.IdUsuario
		                            INNER JOIN TB_Segmento AS Segm ON Invest.IdSegmento = Segm.Id
                            WHERE
	                            Users.CPF = @CPF";

            var lookupUsuario = new Dictionary<string, UsuarioModel>();

            await connection.QueryAsync<UsuarioModel, InvestimentoModel, SegmentoModel, UsuarioModel>(query,
                (usuario, investimento, segmento) =>
                {
                    if (!lookupUsuario.TryGetValue(usuario.CPF!, out var usuarioExiste))
                    {
                        usuarioExiste = usuario;
                        lookupUsuario.Add(usuario.CPF!, usuarioExiste);
                    }

                    usuarioExiste.ListaInvestimento ??= new List<InvestimentoModel>();

                    if (investimento != null)
                    {
                        investimento.Segmento = segmento;
                        usuarioExiste.ListaInvestimento.Add(investimento);
                    }

                    return null!;

                },
                new { CPF },
                splitOn: "Id");

            return lookupUsuario.Values.ToList();
        }
    }
}
