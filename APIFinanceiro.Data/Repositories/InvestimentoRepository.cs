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
	                            Invest.Id,
	                            Invest.Saldo,
	                            Invest.ValorRendimento,
	                            Invest.ValorFinal,
                                Segm.Id,
	                            Segm.TipoSegmento,
	                            Segm.PercentualRendimento,
	                            Segm.TaxaAdm,
	                            Segm.MesesVigencia
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

        public async Task<InvestimentoModel> RetornaInvestimentoUsuarioSegmento(int idUsuario, int idSegmento)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBFinanceiro");

            string query = @"
						    SELECT
	                            *
                            FROM
                                TB_Investimento
                            WHERE
	                            IdUsuario = @IdUsuario
                                AND IdSegmento = @IdSegmento";

            return await connection.QueryFirstOrDefaultAsync<InvestimentoModel>(query,
                new
                { 
                    IdUsuario = idUsuario, 
                    IdSegmento = idSegmento 
                });
        }

        public async Task<int> Aplicar(AplicacaoModel aplicacao)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBFinanceiro");

            string query = @"
						    INSERT INTO TB_Aplicacao
							    (IdInvestimento, Valor, DataAplicacao)
						    VALUES
							    (@IdInvestimento, @Valor, @DataAplicacao);
                            SELECT SCOPE_IDENTITY()";

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    aplicacao.Id = await connection.QueryFirstOrDefaultAsync<int>(query, aplicacao, transaction: transaction);
                    transaction.Commit();
                    return aplicacao.Id;
                }
                catch
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }

        public async Task<int> Resgatar(ResgateModel resgate)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBFinanceiro");

            string query = @"
						    INSERT INTO TB_Resgate
							    (IdInvestimento, Valor, DataResgate)
						    VALUES
							    (@IdInvestimento, @Valor, @DataResgate);
                            SELECT SCOPE_IDENTITY()";

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    resgate.Id = await connection.QueryFirstOrDefaultAsync<int>(query, resgate, transaction: transaction);
                    transaction.Commit();
                    return resgate.Id;
                }
                catch
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }

        public async Task<int> CadastroInvestimento(InvestimentoModel investimento)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBFinanceiro");

            string query = @"
						    INSERT INTO TB_Investimento
							    (IdUsuario, IdSegmento, Saldo, ValorRendimento, ValorFinal)
						    VALUES
							    (@IdUsuario, @IdSegmento, 0, 0, 0);
                            SELECT SCOPE_IDENTITY()";

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    investimento.Id = await connection.QueryFirstOrDefaultAsync<int>(query, investimento, transaction: transaction);
                    transaction.Commit();
                    return investimento.Id;
                }
                catch
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }

        public async Task<bool> AtualizarInvestimento(InvestimentoModel invesimento)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBFinanceiro");
            string query = @"
						    UPDATE
							    TB_Investimento
						    SET
							    IdUsuario = @IdUsuario,
                                IdSegmento = @IdSegmento,
                                Saldo = @Saldo,
                                ValorRendimento = @ValorRendimento,
                                ValorFinal = @ValorFinal
						    WHERE
							    Id = @Id";

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var retorno = await connection.ExecuteAsync(query, invesimento, transaction: transaction) > 0;
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
