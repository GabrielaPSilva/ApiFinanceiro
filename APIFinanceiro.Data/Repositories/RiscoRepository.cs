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
    public class RiscoRepository : IRiscoRepository
    {
        private readonly IDbSession _dbSession;

        public RiscoRepository(IDbSession dbSession)
        {
            _dbSession = dbSession;
        }

        public async Task<List<RiscoModel>> ListarRisco()
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBFinanceiro");

            string query = @"
						    SELECT
							    *
						    FROM
							    TB_Risco
                            ORDER BY
                                GrauRisco";

            return (await connection.QueryAsync<RiscoModel>(query)).ToList();
        }

        public async Task<RiscoModel> RetornarRiscoDescricao(string descricao)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBFinanceiro");

            string query = @"
						     SELECT
							     *
						     FROM
							     TB_Risco
						     WHERE
                                 Descricao = @Descricao";

            return await connection.QueryFirstOrDefaultAsync<RiscoModel>(query, new { Descricao = descricao});
        }

        public async Task<int> CadastrarRisco(RiscoModel risco)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBFinanceiro");

            string query = @"
						    INSERT INTO TB_Risco
							    (GrauRisco, Descricao)
						    VALUES
							    (@GrauRisco, @Descricao)";

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    risco.Id = await connection.QueryFirstOrDefaultAsync<int>(query, risco, transaction: transaction);
                    transaction.Commit();
                    return risco.Id;
                }
                catch
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }

        public async Task<bool> AlterarRisco(RiscoModel risco)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBFinanceiro");
            string query = @"
						    UPDATE
							    TB_Risco
						    SET
                                GrauRisco = @GrauRisco,
                                Descricao = @Descricao
						    WHERE
							    Id = @Id";

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var retorno = await connection.ExecuteAsync(query, risco, transaction: transaction) > 0;
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

        public async Task<bool> RemoverRisco(int idRisco)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBFinanceiro");
            string query = @"
                            DELETE
        	                    TB_Risco
                            WHERE
        	                    Id = @Id";

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var retorno = await connection.ExecuteAsync(query, new { Id = idRisco }, transaction: transaction) > 0;
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
