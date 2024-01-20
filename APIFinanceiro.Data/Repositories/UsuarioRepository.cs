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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDbSession _dbSession;

        public UsuarioRepository(IDbSession dbSession)
        {
            _dbSession = dbSession;
        }

        public async Task<List<UsuarioModel>> ListarUsuario()
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBFinanceiro");

            string query = @"
						    SELECT
	                            *
                            FROM
	                            TB_Usuario AS Users
		                            INNER JOIN TB_Risco AS Risc ON Users.IdRisco = Risc.Id
                            WHERE
	                            Users.Ativo = 1
                            ORDER BY
	                            Users.Nome";

            var lookupUsuario = new Dictionary<int, UsuarioModel>();

            await connection.QueryAsync<UsuarioModel, RiscoModel, UsuarioModel>(query,
                (usuario, risco) =>
                {
                    if (!lookupUsuario.TryGetValue(usuario.Id, out var usuarioExistente))
                    {
                        usuarioExistente = usuario;
                        lookupUsuario.Add(usuario.Id, usuarioExistente);
                    }

                    usuarioExistente.Risco = risco;

                    return null!;
                },
                splitOn: "Id");

            return lookupUsuario.Values.ToList();
        }

        public async Task<UsuarioModel> RetornarUsuarioCPF(string CPF)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBFinanceiro");

            string query = @"
						    SELECT
	                            *
                            FROM
	                            TB_Usuario AS Users
		                            INNER JOIN TB_Risco AS Risc ON Users.IdRisco = Risc.Id
                            WHERE
	                            Users.Ativo = 1
                                AND Users.CPF = @CPF
                            ORDER BY
	                            Users.Nome";


            return (await connection.QueryAsync<UsuarioModel, RiscoModel, UsuarioModel>(query,
                (usuario, risco) =>
                {
                    usuario.Risco = risco;
                    return usuario;
                },
                new { CPF },
                splitOn: "Id")).FirstOrDefault()!;
        }

        public async Task<int> CadastrarUsuario(UsuarioModel usuario)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBFinanceiro");

            string query = @"
						    INSERT INTO TB_Usuario
							    (IdRisco, Nome, Email, Telefone, CPF, DataNascimento)
						    VALUES
							    (@IdRisco, @Nome, @Email, @Telefone, @CPF, @DataNascimento);
                            SELECT SCOPE_IDENTITY()";

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    usuario.Id = await connection.QueryFirstOrDefaultAsync<int>(query, usuario, transaction: transaction);
                    transaction.Commit();
                    return usuario.Id;
                }
                catch
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }

        public async Task<bool> AlterarUsuario(UsuarioModel usuario)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBFinanceiro");
            string query = @"
						    UPDATE
							    TB_Usuario
						    SET
							    IdRisco = @IdRisco,
                                Nome = @Nome,
                                Email = @Email,
                                Telefone = @Telefone,
                                CPF = @CPF
						    WHERE
							    Id = @Id";

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var retorno = await connection.ExecuteAsync(query, usuario, transaction: transaction) > 0;
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

        public async Task<bool> DesativarUsuario(int idUsuario)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBFinanceiro");
            string query = @"
                            UPDATE
        	                    TB_Usuario
                            SET
        	                    Ativo = 0
                            WHERE
        	                    Id = @Id";

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var retorno = await connection.ExecuteAsync(query, new { Id = idUsuario }, transaction: transaction) > 0;
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

        public async Task<bool> DesativarUsuarioCPF(string CPF)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBFinanceiro");
            string query = @"
                            UPDATE
        	                    TB_Usuario
                            SET
        	                    Ativo = 0
                            WHERE
        	                    CPF = @CPF";

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var retorno = await connection.ExecuteAsync(query, new { CPF }, transaction: transaction) > 0;
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

        public async Task<bool> ReativarUsuario(string CPF)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBFinanceiro");
            string query = @"
                            UPDATE
        	                    TB_Usuario
                            SET
        	                    Ativo = 1
                            WHERE
        	                    CPF = @CPF";

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var retorno = await connection.ExecuteAsync(query, new { CPF }, transaction: transaction) > 0;
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
