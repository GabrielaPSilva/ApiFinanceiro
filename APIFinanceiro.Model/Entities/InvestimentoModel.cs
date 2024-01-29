using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIFinanceiro.Model.Entities
{
    public class InvestimentoModel
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdSegmento { get; set; }
        public Decimal Saldo { get; set; }
        public Decimal ValorRendimento { get; set; }
        public Decimal ValorFinal { get; set; }

        public SegmentoModel? Segmento { get; set; }
        public AplicacaoModel? Aplicacao { get; set; }
        public ResgateModel? Resgate { get; set; }

        public bool IsValid(out string mensagemErro)
        {
            bool isValid = true;
            StringBuilder sbMensagemErro = new StringBuilder();

            sbMensagemErro.Clear();

            string mensagemErroDecimal = string.Empty;
            isValid = isValid && ValidarDecimal(out mensagemErroDecimal);
            sbMensagemErro.Append(mensagemErroDecimal);

            mensagemErro = sbMensagemErro.ToString();

            return isValid;
        }
        private bool ValidarDecimal(out string mensagemErro)
        {
            bool isValidSaldo = true;
            bool isValidValorRendimento = true;
            bool isValidValorFinal = true;
            isValidSaldo = Saldo!.GetType() == typeof(decimal);
            isValidValorRendimento = ValorRendimento!.GetType() == typeof(decimal);
            isValidValorFinal = ValorFinal.GetType() == typeof(decimal);

            mensagemErro = string.Empty;

            if (!isValidSaldo)
            {
                mensagemErro = "O saldo deve ser um decimal.\n";
                return isValidSaldo;
            }


            if (!isValidValorRendimento)
            {
                mensagemErro = "O valor de rendimento deve ser um decimal.\n";
                return isValidValorRendimento;
            }


            if (!isValidValorFinal)
            {
                mensagemErro = "O valor de rendimento deve ser um decimal.\n";
                return isValidValorFinal;
            }

            return true;
        }
    }
}
