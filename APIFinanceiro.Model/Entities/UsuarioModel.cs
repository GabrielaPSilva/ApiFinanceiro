using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace APIFinanceiro.Model.Entities
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public int IdRisco { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public string? CPF { get; set; }
        public string? DataNascimento { get; set; }
        public bool Ativo { get; set; }

        public List<InvestimentoModel>? ListaInvestimento { get; set; }
        public RiscoModel? Risco { get; set; }

        public bool IsValid(out string mensagemErro)
        {
            bool isValid = true;
            StringBuilder sbMensagemErro = new StringBuilder();

            sbMensagemErro.Clear();

            string mensagemErroNome = string.Empty;
            isValid = isValid && ValidarNome(out mensagemErroNome);
            sbMensagemErro.Append(mensagemErroNome);

            string mensagemErroEmail = string.Empty;
            isValid = isValid && ValidarEmail(out mensagemErroEmail);
            sbMensagemErro.Append(mensagemErroEmail);

            string mensagemErroTelefone = string.Empty;
            isValid = isValid && ValidarTelefone(out mensagemErroTelefone);
            sbMensagemErro.Append(mensagemErroTelefone);

            string mensagemErroCPF = string.Empty;
            isValid = isValid && ValidarCPF(out mensagemErroCPF);
            sbMensagemErro.Append(mensagemErroCPF);

            string mensagemErroNascimento = string.Empty;
            isValid = isValid && ValidarDataNascimento(out mensagemErroNascimento);
            sbMensagemErro.Append(mensagemErroNascimento);

            mensagemErro = sbMensagemErro.ToString();

            return isValid;
        }

        private bool ValidarNome(out string mensagemErro)
        {
            bool isValid = true;
            isValid = isValid && !string.IsNullOrEmpty(Nome);
            isValid = isValid && Nome!.Length >= 1;
            isValid = isValid && Nome!.Length <= 100;

            mensagemErro = string.Empty;

            if (!isValid)
                mensagemErro = "O nome deve ter entre 1 e 100 caracteres.\n";

            return isValid;
        }

        private bool ValidarEmail(out string mensagemErro)
        {
            const string EmailRegexPattern = @"^[\w\.-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,})$";

            bool isValid = true;
            isValid = isValid && !string.IsNullOrEmpty(Email);
            isValid = isValid && Email!.Length >= 1;
            isValid = isValid && Email!.Length <= 100;
            isValid = isValid && Regex.IsMatch(Email!, EmailRegexPattern);

            mensagemErro = string.Empty;

            if (!isValid)
                mensagemErro = "Informe um endereço de e-mail válido.";

            return isValid;
        }

        private bool ValidarTelefone(out string mensagemErro)
        {
            bool isValid = true;

            string TelefoneRegexPattern = @"^\d{2}\d{8,9}$";

            isValid = isValid && !string.IsNullOrEmpty(Telefone);
            isValid = isValid && Telefone!.Length >= 1;
            isValid = isValid && Telefone!.Length <= 20;
            isValid = isValid && Regex.IsMatch(Telefone!, TelefoneRegexPattern);

            mensagemErro = string.Empty;

            if (!isValid)
                mensagemErro = "Informe um telefone válido.";

            return isValid;
        }

        private bool ValidarCPF(out string mensagemErro)
        {
            bool isValid = true;
            StringBuilder sbMensagemErro = new StringBuilder();

            string cpf = CPF!;

            string cpfLimpo = new string(cpf.Where(char.IsDigit).ToArray());

            string cpfRegexPattern = @"^\d{11}$";

            isValid = isValid && !string.IsNullOrEmpty(cpf);
            isValid = isValid && cpfLimpo.Length == 11;
            isValid = isValid && Regex.IsMatch(cpfLimpo, cpfRegexPattern);

            if (!isValid)
                sbMensagemErro.AppendLine("Informe um CPF válido.");

            mensagemErro = sbMensagemErro.ToString().TrimEnd();
            return isValid;
        }

        private bool ValidarDataNascimento(out string mensagemErro)
        {
            bool isValid = true;
            StringBuilder sbMensagemErro = new StringBuilder();

            string dataNascimento = DataNascimento!;

            string dataRegexPattern = @"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$";

            isValid = isValid && !string.IsNullOrEmpty(dataNascimento);
            isValid = isValid && Regex.IsMatch(dataNascimento, dataRegexPattern);

            if (isValid)
            {
                if (!DateTime.TryParseExact(dataNascimento, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                {
                    isValid = false;
                    sbMensagemErro.AppendLine("Informe a data de nascimento como yyyy-MM-dd");
                }
            }
            else
            {
                sbMensagemErro.AppendLine("Informe uma data de nascimento válida.");
            }

            mensagemErro = sbMensagemErro.ToString().TrimEnd();
            return isValid;
        }
    }
}
