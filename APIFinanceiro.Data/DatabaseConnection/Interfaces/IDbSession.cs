using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFinanceiro.Data.DatabaseConnection.Interfaces
{
    public interface IDbSession
    {
        Task<IDbConnection> GetConnectionAsync(string banco);
    }
}
