using APIFinanceiro.Data.DatabaseConnection;
using APIFinanceiro.Data.DatabaseConnection.Interfaces;
using Microsoft.AspNetCore.Http;

namespace APIFinanceiro.Initializers
{
    public class SessionInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<IDbSession, DbSession>();
        }
    }
}
