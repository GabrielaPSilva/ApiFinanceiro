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
							    TB_Segmento
                            ORDER BY
                                TipoSegmento";

            return (await connection.QueryAsync<SegmentoModel>(query)).ToList();
        }

        public async Task<SegmentoModel> RetornarSegmentoTipoSegmento(string tipoSegmento)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBFinanceiro");

            string query = @"
						     SELECT
							     *
						     FROM
							     TB_Segmento
						     WHERE
                                 TipoSegmento = @TipoSegmento";

            return await connection.QueryFirstOrDefaultAsync<SegmentoModel>(query, new { TipoSegmento = tipoSegmento });
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
