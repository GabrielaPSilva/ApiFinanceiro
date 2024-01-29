using APIFinanceiro.Data.Repositories;
using APIFinanceiro.Data.Repositories.Interfaces;

namespace APIFinanceiro.Initializers
{
    public class DataInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            //Significa que uma nova instância de serviço é criada cada vez que for solicitada, é adequado para serviços leves e sem estado, onde não há necessidade de manter estado ou compartilhamento de recursos.
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IRiscoRepository, RiscoRepository>();
            services.AddTransient<ISegmentoRepository, SegmentoRepository>();
            services.AddTransient<IInvestimentoRepository, InvestimentoRepository>();
        }
    }
}
