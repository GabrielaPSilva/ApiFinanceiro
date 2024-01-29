using APIFinanceiro.Data.DatabaseConnection.Interfaces;
using APIFinanceiro.Data.Repositories.Interfaces;
using APIFinanceiro.Model;
using APIFinanceiro.Model.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFinanceiro.Data.Repositories
{
    public class AutenticacaoRepository : IAutenticacaoRepository
    {
        private readonly IDbSession _dbSession;

        public AutenticacaoRepository(IDbSession dbSession)
        {
            _dbSession = dbSession;
        }

        public async Task<UsuarioPermissaoModel> ObterUsuarioNomeSenha(string nome, string senha)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBFinanceiro");

            string query = @"
						     SELECT
							     *
						     FROM
							     TB_Usuario_Permissao
						     WHERE
                                Nome = @Nome 
                                AND Senha = @Senha";

            return await connection.QueryFirstOrDefaultAsync<UsuarioPermissaoModel>(query, new { Nome = nome, Senha = senha });
        }
    }
}
