using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace APIFinanceiro.Model.Entities
{
    public class ResgateModel
    {
        public int Id { get; set; }
        public int IdInvestimento { get; set; }
        public Decimal Valor { get; set; }
        public string? DataResgate { get; set; }
        public bool IsValid(out string mensagemErro)
        {
            bool isValid = true;
            StringBuilder sbMensagemErro = new StringBuilder();

            sbMensagemErro.Clear();

            string mensagemErroValor = string.Empty;
            isValid = isValid && ValidarValor(out mensagemErroValor);
            sbMensagemErro.Append(mensagemErroValor);

            string mensagemErroDataResgate = string.Empty;
            isValid = isValid && ValidarDataResgate(out mensagemErroDataResgate);
            sbMensagemErro.Append(mensagemErroDataResgate);

            mensagemErro = sbMensagemErro.ToString();

            return isValid;
        }

        private bool ValidarValor(out string mensagemErro)
        {
            bool isValid = true;
            isValid = Valor!.GetType() == typeof(decimal);

            mensagemErro = string.Empty;

            if (!isValid)
                mensagemErro = "O valor do resgate deve ser um decimal.\n";

            return isValid;
        }

        private bool ValidarDataResgate(out string mensagemErro)
        {
            bool isValid = true;
            StringBuilder sbMensagemErro = new StringBuilder();

            string dataNascimento = DataResgate!;

            string dataRegexPattern = @"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$";

            isValid = isValid && !string.IsNullOrEmpty(dataNascimento);
            isValid = isValid && Regex.IsMatch(dataNascimento, dataRegexPattern);

            if (isValid)
            {
                if (!DateTime.TryParseExact(dataNascimento, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                {
                    isValid = false;
                    sbMensagemErro.AppendLine("Informe a data como yyyy-MM-dd.\n");
                }
            }
            else
            {
                sbMensagemErro.AppendLine("Informe uma data de resgate válida.\n");
            }

            mensagemErro = sbMensagemErro.ToString().TrimEnd();
            return isValid;
        }
    }
}
