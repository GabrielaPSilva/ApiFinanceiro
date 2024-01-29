using APIFinanceiro.Business.Services;
using APIFinanceiro.Business.Services.Interfaces;

namespace APIFinanceiro.Initializers
{
    public class BusinessInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            //Significa que uma nova instância de serviço é criada cada vez que for solicitada, é adequado para serviços leves e sem estado, onde não há necessidade de manter estado ou compartilhamento de recursos.
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IRiscoService, RiscoService>();
            services.AddTransient<ISegmentoService, SegmentoService>();
            services.AddTransient<IInvestimentoService, InvestimentoService>();
            services.AddTransient<IAutenticacaoService, AutenticacaoService>();
        }
    }
}
