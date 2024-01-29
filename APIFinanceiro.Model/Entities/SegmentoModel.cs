using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
    }
}
