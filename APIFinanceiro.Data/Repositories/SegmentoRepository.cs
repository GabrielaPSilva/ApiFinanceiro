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
    public class SegmentoRepository : ISegmentoRepository
    {
        private readonly IDbSession _dbSession;

        public SegmentoRepository(IDbSession dbSession)
        {
            _dbSession = dbSession;
        }

        public async Task<List<SegmentoModel>> ListarSegmento()
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBFinanceiro");

            string query = @"
						    SELECT 
	                            *
                            FROM
	                            TB_Segmento AS Segm
		                            INNER JOIN TB_Risco AS Risc ON
			                            Segm.IdRisco = Risc.Id
                            ORDER BY
                                Segm.TipoSegmento";

            var lookupSegmento = new Dictionary<int, SegmentoModel>();

            await connection.QueryAsync<SegmentoModel, RiscoModel, SegmentoModel>(query,
                (segmento, risco) =>
                {
                    if (!lookupSegmento.TryGetValue(segmento.Id, out var segmentoExistente))
                    {
                        segmentoExistente = segmento;
                        lookupSegmento.Add(segmento.Id, segmentoExistente);
                    }

                    segmentoExistente.Risco = risco;

                    return null!;
                },
                splitOn: "Id");

            return lookupSegmento.Values.ToList();
        }

        public async Task<List<SegmentoModel>> RetornarSegmentoTipoSegmento(string tipoSegmento)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBFinanceiro");

            string query = @"
						     SELECT 
	                            *
                             FROM
	                            TB_Segmento AS Segm
		                            INNER JOIN TB_Risco AS Risc ON
			                            Segm.IdRisco = Risc.Id
						     WHERE
                                Segm.TipoSegmento LIKE '%' + @TipoSegmento + '%'";

            var lookupSegmento = new Dictionary<int, SegmentoModel>();

            await connection.QueryAsync<SegmentoModel, RiscoModel, SegmentoModel>(query,
                (segmento, risco) =>
                {
                    if (!lookupSegmento.TryGetValue(segmento.Id, out var segmentoExistente))
                    {
                        segmentoExistente = segmento;
                        lookupSegmento.Add(segmento.Id, segmentoExistente);
                    }

                    segmentoExistente.Risco = risco;

                    return null!;
                },
                new { TipoSegmento = tipoSegmento },
                splitOn: "Id");

            return lookupSegmento.Values.ToList();
        }

        public async Task<SegmentoModel> RetornarSegmentoTipoSegmentoIdRisco(string tipoSegmento, int idRisco)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBFinanceiro");

            string query = @"
						     SELECT 
	                            *
                             FROM
	                            TB_Segmento AS Segm
		                            INNER JOIN TB_Risco AS Risc ON
			                            Segm.IdRisco = Risc.Id
						     WHERE
                                 Segm.TipoSegmento LIKE '%' + @TipoSegmento + '%'
                                 AND Segm.IdRisco = @IdRisco";

            return (await connection.QueryAsync<SegmentoModel, RiscoModel, SegmentoModel>(query,
                      (segmento, risco) =>
                      {
                          segmento.Risco = risco;
                          return segmento;
                      },
                      new
                      {
                          TipoSegmento = tipoSegmento,
                          IdRisco = idRisco
                      },
                      splitOn: "Id")).FirstOrDefault()!;
        }

        public async Task<int> CadastrarSegmento(SegmentoModel segmento)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBFinanceiro");

            string query = @"
						    INSERT INTO TB_Segmento
							    (IdRisco, TipoSegmento, PercentualRendimento, TaxaAdm, MesesVigencia)
						    VALUES
							    (@IdRisco, @TipoSegmento, @PercentualRendimento, @TaxaAdm, @MesesVigencia);
                            SELECT SCOPE_IDENTITY()";

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    segmento.Id = await connection.QueryFirstOrDefaultAsync<int>(query, segmento, transaction: transaction);
                    transaction.Commit();
                    return segmento.Id;
                }
                catch
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }

        public async Task<bool> AlterarSegmento(SegmentoModel segmento)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBFinanceiro");
            string query = @"
						    UPDATE
							    TB_Segmento
						    SET
                                IdRisco              = @IdRisco,
                                TipoSegmento         = @TipoSegmento,
                                PercentualRendimento = @PercentualRendimento,
                                TaxaAdm              = @TaxaAdm,
                                MesesVigencia        = @MesesVigencia
						    WHERE
							    Id = @Id";

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var retorno = await connection.ExecuteAsync(query, segmento, transaction: transaction) > 0;
                    transaction.Commit();
                    return retorno;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> RemoverSegmento(int idSegmento)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBFinanceiro");
            string query = @"
                            DELETE
        	                    TB_Segmento
                            WHERE
        	                    Id = @Id";

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var retorno = await connection.ExecuteAsync(query, new { Id = idSegmento }, transaction: transaction) > 0;
                    transaction.Commit();
                    return retorno;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
