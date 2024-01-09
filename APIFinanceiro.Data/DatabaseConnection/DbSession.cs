using APIFinanceiro.Data.DatabaseConnection.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFinanceiro.Data.DatabaseConnection
{
    public class DbSession : IDbSession
    {
        public IDbConnection? Connection { get; set; }

        public async Task<bool> GenerateSessionDB(string banco)
        {
            Connection = await ConnectionFactory.ConnectionFactory.ConnectionDBAsync(banco);

            if (Connection.State != ConnectionState.Open)
                Connection.Open();

            return Connection != null;
        }

        public async Task<IDbConnection> GetConnectionAsync(string banco)
        {
            if (Connection == null)
            {
                await GenerateSessionDB(banco);
            }

            Connection!.ChangeDatabase(banco);

            return Connection;
        }
    }
}
