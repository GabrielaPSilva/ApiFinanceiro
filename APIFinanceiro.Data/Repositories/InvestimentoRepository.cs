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

        //public async Task<List<ClienteModel>> ListarClientesTelefones()
        //{
        //    IDbConnection connection = await _dbSession.GetConnectionAsync("DBCliente");
        //    string query = @"
						  //  SELECT 
	       //                     Users.Id,
	       //                     Users.Nome,
	       //                     Users.CPF,
	       //                     Users.Email,
	       //                     Segm.Id,
	       //                     Segm.TipoSegmento,
	       //                     Segm.PercentualRendimento,
	       //                     Segm.TaxaAdm,
	       //                     Segm.MesesVigencia,
	       //                     Invest.Id,
	       //                     Invest.Saldo,
	       //                     Invest.ValorRendimento,
	       //                     Invest.ValorFinal
        //                    FROM 
	       //                     TB_Investimento AS Invest
		      //                      INNER JOIN TB_Usuario AS Users ON Invest.IdUsuario = Users.Id
		      //                      INNER JOIN TB_Segmento AS Segm ON Invest.IdSegmento = Segm.Id
        //                    WHERE
	       //                     TB_Usuario.CPF = @CPF
        //                        AND TB_Usuario.Ativo = 1";

        //    var lookupCliente = new Dictionary<int, ClienteModel>();

        //    await connection.QueryAsync<ClienteModel, TelefoneClienteModel, TipoTelefoneModel, ClienteModel>(query,
        //        (cliente, telefone, tipoTelefone) =>
        //        {
        //            if (!lookupCliente.TryGetValue(cliente.Id, out var clienteExistente))
        //            {
        //                clienteExistente = cliente;
        //                lookupCliente.Add(cliente.Id, clienteExistente);
        //            }

        //            clienteExistente.ListaTelefones ??= new List<TelefoneClienteModel>();

        //            if (telefone != null)
        //            {
        //                telefone.TipoTelefone = tipoTelefone;
        //                clienteExistente.ListaTelefones.Add(telefone);
        //            }

        //            return null!;

        //        }, splitOn: "Id");

        //    return lookupCliente.Values.ToList();
        //}
    }
}
