using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFinanceiro.Model.Entities
{
    public class AplicacaoModel
    {
        public int Id { get; set; }
        public int IdInvestimento { get; set; }
        public Decimal Valor { get; set; }
        public DateTime DataAplicacao { get; set; }

        public bool IsValid(out string mensagemErro)
        {
            bool isValid = true;
            StringBuilder sbMensagemErro = new StringBuilder();

            sbMensagemErro.Clear();

            string mensagemErroValor = string.Empty;
            isValid = isValid && ValidarValor(out mensagemErroValor);
            sbMensagemErro.Append(mensagemErroValor);

            string mensagemErroDataAplicacao = string.Empty;
            isValid = isValid && ValidarDataAplicacao(out mensagemErroDataAplicacao);
            sbMensagemErro.Append(mensagemErroDataAplicacao);

            mensagemErro = sbMensagemErro.ToString();

            return isValid;
        }

        private bool ValidarValor(out string mensagemErro)
        {
            bool isValid = true;
            isValid = Valor!.GetType() == typeof(decimal);

            mensagemErro = string.Empty;

            if (!isValid)
                mensagemErro = "O Valor da aplicação deve ser um decimal.\n";

            return isValid;
        }

        private bool ValidarDataAplicacao(out string mensagemErro)
        {
            bool isValid = true;
            isValid = DataAplicacao!.GetType() == typeof(DateTime);

            mensagemErro = string.Empty;

            if (!isValid)
                mensagemErro = "O Valor da aplicação deve ser um DateTime dd/MM/yyyy HH:mm:ss.\n";

            return isValid;
        }
    }
}
