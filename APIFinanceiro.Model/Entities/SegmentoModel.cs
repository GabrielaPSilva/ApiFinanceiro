using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace APIFinanceiro.Model.Entities
{
    public class SegmentoModel
    {
        public int Id { get; set; }
        public int IdRisco { get; set; }
        public string? TipoSegmento { get; set; }
        public Decimal PercentualRendimento { get; set; }
        public Decimal TaxaAdm { get; set; }
        public int MesesVigencia { get; set; }

        public RiscoModel? Risco { get; set; }

        public bool IsValid(out string mensagemErro)
        {
            bool isValid = true;
            StringBuilder sbMensagemErro = new StringBuilder();

            sbMensagemErro.Clear();

            string mensagemErroNome = string.Empty;
            isValid = isValid && ValidarTipoSegmento(out mensagemErroNome);
            sbMensagemErro.Append(mensagemErroNome);

            string mensagemErroPercentual = string.Empty;
            isValid = isValid && ValidarPercentualRendimento(out mensagemErroPercentual);
            sbMensagemErro.Append(mensagemErroPercentual);

            string mensagemErroTaxaAdm = string.Empty;
            isValid = isValid && ValidarTaxaAdm(out mensagemErroTaxaAdm);
            sbMensagemErro.Append(mensagemErroTaxaAdm);

            string mensagemErroMesesVigencia = string.Empty;
            isValid = isValid && ValidarMesesVigencia(out mensagemErroMesesVigencia);
            sbMensagemErro.Append(mensagemErroMesesVigencia);

            mensagemErro = sbMensagemErro.ToString();

            return isValid;
        }

        private bool ValidarTipoSegmento(out string mensagemErro)
        {
            bool isValid = true;
            isValid = isValid && !string.IsNullOrEmpty(TipoSegmento);
            isValid = isValid && TipoSegmento!.Length >= 1;
            isValid = isValid && TipoSegmento!.Length <= 100;

            mensagemErro = string.Empty;

            if (!isValid)
                mensagemErro = "O tipo de segmento deve ter entre 1 e 100 caracteres.\n";

            return isValid;
        }

        private bool ValidarPercentualRendimento(out string mensagemErro)
        {
            bool isValid = true;
            isValid = PercentualRendimento!.GetType() == typeof(decimal);

            mensagemErro = string.Empty;

            if (!isValid)
                mensagemErro = "O percentual de rendimento deve ser um decimal.\n";

            return isValid;
        }

        private bool ValidarTaxaAdm(out string mensagemErro)
        {
            bool isValid = true;
            isValid = TaxaAdm!.GetType() == typeof(decimal);

            mensagemErro = string.Empty;

            if (!isValid)
                mensagemErro = "A taxa de administração deve ser um decimal.\n";

            return isValid;
        }
        private bool ValidarMesesVigencia(out string mensagemErro)
        {
            bool isValid = true;
            isValid = MesesVigencia!.GetType() == typeof(int);

            mensagemErro = string.Empty;

            if (!isValid)
                mensagemErro = "Os meses de vigência deve ser um inteiro.\n";

            return isValid;
        }

    }
}
